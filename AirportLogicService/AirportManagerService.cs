using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Models;

namespace AirportLogicService
{
    public class AirportManagerService : IAirportManager
    {
        Random rnd;
        Airport airport;
        private int timeSpan;
        Timer timer;
        public AirportManagerService()
        {
            rnd = new Random();
            timeSpan = rnd.Next(1000, 5000);
            timer = new Timer(timeSpan);
            timer.Elapsed += Timer_Elapsed;
            airport = new Airport();
        }

        //change timer type
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            MoveToNextStation((Flight)sender);
        }

        //assuming the airplane is already in a station...
        public void MoveToNextStation(Flight flight)
        {
            Station nextStation;

            if (flight.State == State.Landing)
            {
                //checking if it is the last station
                foreach (var st in airport.LastLandingStations)
                {
                    if (flight.CurrentStation.Equals(st))
                    {
                        flight.CurrentStation.FlightsQueue.Dequeue();
                        return;
                    }
                }

                nextStation = GetShortestQueue(flight.CurrentStation.NextLandingStations);
                nextStation.FlightsQueue.Enqueue(flight);
                if (nextStation.FlightsQueue.Count == 1)
                {
                    flight.CurrentStation.FlightsQueue.Dequeue();
                    flight.CurrentStation = nextStation;
                    timer.Start();
                }
            }
            else if (flight.State == State.TakingOff)
            {
                //checking if it is the last station
                foreach (var st in airport.LastFlyingStations)
                {
                    if (flight.CurrentStation.Equals(st))
                    {
                        flight.CurrentStation.FlightsQueue.Dequeue();
                        return;
                    }
                }

                nextStation = GetShortestQueue(flight.CurrentStation.NextFlyingStations);
                nextStation.FlightsQueue.Enqueue(flight);
                if (nextStation.FlightsQueue.Count == 1)
                {
                    flight.CurrentStation.FlightsQueue.Dequeue();
                    flight.CurrentStation = nextStation;
                    timer.Start();
                }

            }
        }

        public Station GetShortestQueue(List<Station> stations)
        {
            Station shortest = stations.First();

            foreach (Station st in stations)
            {
                if (st.FlightsQueue.Count < shortest.FlightsQueue.Count)
                    shortest = st;
            }

            return shortest;
        }


        public void StartLanding(Flight flight)
        {
            //foreach (Station station in airport.FirstLandingStations)
            //{
            //    if (station.FlightInStation != null)
            //    {
            //        station.FlightInStation = flight;
            //        break;
            //    }
            //}
        }

        public void StartTakingOff(Flight flight)
        {
            throw new NotImplementedException();
        }
    }
}
