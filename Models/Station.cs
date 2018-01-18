using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Station
    {
        public int ID { get; set; }
        //public Flight FlightInStation { get; set; }
        public List<Station> NextLandingStations { get; set; }
        public List<Station> NextFlyingStations { get; set; }
        public Queue<Flight> FlightsQueue { get; set; }

        public Station(int id)
        {
            ID = id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Station other = (Station)obj;
            return ID==other.ID;
        }
    }
}
