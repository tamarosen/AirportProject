using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AirportLogicService.ContractTypes;
using AirportLogicService.RepoServiceReference;
using Models;

namespace AirportLogicService
{
    public class AirportManagerService : IAirportManager
    {
        private Random rnd;
        private Airport airport;
        private Timer scheduleTimer;
        private SortedSet<DTOs.FlightDTO> flights;
        private RepositoryClient repo;

        public AirportManagerService()
        {
            repo = new RepositoryClient();

            flights = new SortedSet<DTOs.FlightDTO>(repo.GetFutureFlights(), new FlightsTimeComparer());
            
            TimeSpan firstFlight = GetNextTimeSpan();
            scheduleTimer = new Timer(HandleFlightScedule, null, new TimeSpan(0), firstFlight);

            airport = new Airport();
        }

        private void HandleFlightScedule(object state)
        {
            Flight fl = GetFlight();
            if (fl.State == Models.State.Landing)
            {
                StartLanding(fl);
            }
            else
            {
                StartTakingOff(fl);
            }
            TimeSpan timeUntilNextFlight = GetNextTimeSpan();
            scheduleTimer.Change(new TimeSpan(0), timeUntilNextFlight);
        }

        private Flight GetFlight()
        {
            DTOs.FlightDTO flightDTO = flights.First();
            Flight flight = new Flight()
            {
                ID = flightDTO.ID,
                State = (Models.State)Enum.Parse(typeof(Models.State), flightDTO.State.ToString()),
                CurrentStation = null,
                StartRouteTime = flightDTO.StartRouteTime
            };
            flights.Remove(flightDTO);
            return flight;
        }

        private TimeSpan GetNextTimeSpan()
        {
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
                    new Timer(MoveToNextStation, flight, rnd.Next(1000, 5000), 0);
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
            Flight flight = station.DequeueFlight();
            if (flight != null)
            {
                Station prevStation = flight.CurrentStation;
                flight.CurrentStation = station;
                new Timer(MoveToNextStation, flight, rnd.Next(1000, 5000), 0);
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
                flight.CurrentStation = st;
                Timer timer = new Timer(MoveToNextStation, flight, rnd.Next(1000, 5000), 0);
            }
        }

        public void StartTakingOff(Flight flight)
        {
            Station st;
            st = GetShortestQueue(airport.FirstFlyingStations);
            // if this is the only flight in station
            if (st.EnqueueFlight(flight))
            {
                flight.CurrentStation = st;
                Timer timer = new Timer(MoveToNextStation, flight, rnd.Next(1000, 5000), 0);
            }
        }
        
        public bool SceduleNewFlight(DTOs.FlightDTO flightDTO)
        {
            flights.Add(flightDTO);
            if (flightDTO.StartRouteTime < flights.First().StartRouteTime)
            {
                TimeSpan timeUntilNextFlight = GetNextTimeSpan();
                scheduleTimer.Change(new TimeSpan(0), timeUntilNextFlight);
            }
            return repo.AddFlightToScedule(flightDTO);
        }
    }
}
