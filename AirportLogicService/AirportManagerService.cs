using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using AirportLogicService.ContractTypes;
using AirportLogicService.RepositoryServiceReference;
using Models;

namespace AirportLogicService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class AirportManagerService : IAirportManager, IDisposable
    {
        private Random rnd;
        private Airport airport;
        private Timer scheduleTimer;
        private SortedSet<DTOs.FlightDTO> flights;
        private RepositoryClient repo;
        private IAirportDuplexCallback sessionCallback;

        public AirportManagerService()
        {
            rnd = new Random();
            repo = new RepositoryClient();
            airport = Airport.Instance();
            InitCallback();
            LoadFutureFlights();
            TimeSpan firstFlight = GetNextTimeSpan();
            scheduleTimer = new Timer(HandleFlightScedule, null, firstFlight, firstFlight);
        }

        private void InitCallback()
        {
            sessionCallback = null;
            OperationContext opCtx = OperationContext.Current;
            if (opCtx != null)
            {
                sessionCallback = opCtx.GetCallbackChannel<IAirportDuplexCallback>();
                CallbacksHolder.Instance().Add(sessionCallback);
            }
        }

        private void LoadFutureFlights()
        {
            flights = new SortedSet<DTOs.FlightDTO>(repo.GetFutureFlights(), new FlightsTimeComparer());
            //if (Callback != null)
            //{
            //    Callback.StationStateUpdate();
            //}
        }

        private void HandleFlightScedule(object state)
        {
            Flight fl = GetFlight();
            if (fl != null)
            {
                if (fl.State == Models.State.Landing)
                {
                    StartLanding(fl);
                }
                else
                {
                    StartTakingOff(fl);
                }
            }
            TimeSpan timeUntilNextFlight = GetNextTimeSpan();
            if (timeUntilNextFlight < new TimeSpan(0))
            {
                timeUntilNextFlight = new TimeSpan(0, 0, 0, 0, 1);
            }
            scheduleTimer.Change(timeUntilNextFlight, timeUntilNextFlight);
        }

        private Flight GetFlight()
        {
            if (flights.Count == 0) return null;
            DTOs.FlightDTO flightDTO = flights.First();
            Flight flight = new Flight()
            {
                ID = flightDTO.ID,
                State = (Models.State)Enum.Parse(typeof(Models.State), flightDTO.State.ToString()),
                CurrentStation = null,
                StartRouteTime = flightDTO.StartRouteTime
            };
            flights.Remove(flightDTO);
            CallbacksHolder.Instance().FlightRemove(flightDTO);
            return flight;
        }

        private TimeSpan GetNextTimeSpan()
        {
            if (flights.Count == 0)
            {
                return new TimeSpan(0, 0, 0, 0, -1);
            }
            TimeSpan timeUntilNextFlight = flights.First().StartRouteTime.Subtract(DateTime.Now);
            return timeUntilNextFlight;
        }


        //assuming the airplane is already in a station...
        public void MoveToNextStation(Object o)
        {
            Flight flight = (Flight)o;
            Station nextStation;

            if (flight.State == Models.State.Landing)
            {
                nextStation = GetShortestQueue(flight.CurrentStation.NextLandingStations);
            }
            else
            {
                nextStation = GetShortestQueue(flight.CurrentStation.NextFlyingStations);

            }

            if (nextStation == null || nextStation.EnqueueFlight(flight))
            {
                Station prevStation = flight.CurrentStation;
                flight.CurrentStation = nextStation;
                if (nextStation != null)
                {
                    CallbacksHolder.Instance().StationStateUpdate(nextStation);
                    new Timer(MoveToNextStation, flight, rnd.Next(10000, 60000), 0);
                }
                else
                {
                    // add to history...
                }
                HandlePrevStation(prevStation);
            }
        }

        private void HandlePrevStation(Station station)
        {
            if (station == null)
            {
                return;
            }
            // pops the flight currently in the station and get the next one
            Flight flight = station.DequeueFlight();
            CallbacksHolder.Instance().StationStateUpdate(station);
            // if there's a next flight
            if (flight != null)
            {
                Station prevStation = flight.CurrentStation;
                flight.CurrentStation = station;
                new Timer(MoveToNextStation, flight, rnd.Next(10000, 60000), 0);
                HandlePrevStation(prevStation);
            }
        }

        public Station GetShortestQueue(List<Station> stations)
        {
            if (stations == null)
            {
                return null;
            }
            Station shortest = stations.First();

            foreach (Station st in stations)
            {
                if (st.GetQueueCount() < shortest.GetQueueCount())
                    shortest = st;
            }

            return shortest;
        }


        public void StartLanding(Flight flight)
        {
            Station st;
            st = GetShortestQueue(airport.FirstLandingStations);
            // if this is the only flight in station
            if (st.EnqueueFlight(flight))
            {
                CallbacksHolder.Instance().StationStateUpdate(st);
                flight.CurrentStation = st;
                Timer timer = new Timer(MoveToNextStation, flight, rnd.Next(10000, 60000), 0);
            }
        }

        public void StartTakingOff(Flight flight)
        {
            Station st;
            st = GetShortestQueue(airport.FirstFlyingStations);
            // if this is the only flight in station
            if (st.EnqueueFlight(flight))
            {
                CallbacksHolder.Instance().StationStateUpdate(st);
                flight.CurrentStation = st;
                Timer timer = new Timer(MoveToNextStation, flight, rnd.Next(10000, 60000), 0);
            }
        }

        public bool ScheduleNewFlight(DTOs.FlightDTO flightDTO)
        {
            //add new flight to the db schedule table and get it back with unique id
            DTOs.FlightDTO newFlight = repo.AddFlightToSchedule(flightDTO);
            DTOs.FlightDTO firstFlight = flights.Count == 0 ? null : flights.First();

            if (newFlight == null) return false;

            if (!flights.Add(newFlight)) return false;

            CallbacksHolder.Instance().FlightAdd(newFlight);
            if (firstFlight == null || newFlight.StartRouteTime < firstFlight.StartRouteTime)
            {
                TimeSpan timeUntilNextFlight = GetNextTimeSpan();
                scheduleTimer.Change(timeUntilNextFlight, timeUntilNextFlight);
            }
            return true;
        }

        public IEnumerable<DTOs.FlightDTO> GetFutureFlights()
        {
            return new List<DTOs.FlightDTO>(flights);
        }

        public IEnumerable<DTOs.StationDTO> GetStationsState()
        {
            List<DTOs.StationDTO> stations = new List<DTOs.StationDTO>();
            foreach (var st in airport.AllStations.Values)
            {
                DTOs.StationDTO stationDTO = new DTOs.StationDTO()
                {
                    StationID = st.ID,
                    FlightID = st.GetFlightInStation()
                };
                stations.Add(stationDTO);
            }
            return stations;
        }

        public void Dispose()
        {
            if (sessionCallback != null)
            {
                CallbacksHolder.Instance().Remove(sessionCallback);
            }
        }
    }
}
