using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Station
    {
        private Queue<Flight> FlightsQueue;
        public int ID { get; private set; }
        //public Flight FlightInStation { get; set; }
        public List<Station> NextLandingStations { get; set; }
        public List<Station> NextFlyingStations { get; set; }

        public Station(int id)
        {
            ID = id;
            FlightsQueue = new Queue<Flight>();
            NextFlyingStations = null;
            NextLandingStations = null;
        }

        // returns true if the plane is in the station (first in the queue)
        public bool EnqueueFlight(Flight flight)
        {
            lock (FlightsQueue)
            {
                FlightsQueue.Enqueue(flight);
                if (FlightsQueue.Count == 1)
                {
                    return true;
                }
                return false;
            }
        }

        // returns the next flight is in the station (first in the queue)
        public Flight DequeueFlight()
        {
            lock (FlightsQueue)
            {
                FlightsQueue.Dequeue();
                if (FlightsQueue.Any())
                {
                    return FlightsQueue.First();
                }
                return null;
            }
        }

        public int GetQueueCount()
        {
            return FlightsQueue.Count();
        }
        
        public int GetFlightInStation()
        {
            if (FlightsQueue.Count == 0) return -1;
            return FlightsQueue.Peek().ID;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Station other = (Station)obj;
            return ID == other.ID;
        }
    }
}
