using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Airport
    {
        static public Airport self;

        public Dictionary<int, Station> AllStations;
        public List<Station> FirstLandingStations { get; private set; }
        public List<Station> FirstFlyingStations { get; private set; }
        public List<Station> LastLandingStations { get; private set; }
        public List<Station> LastFlyingStations { get; private set; }

        // should be a singlton:
        private Airport()
        {
            CreateStations();
        }

        static public Airport Instance()
        {
            if (self == null)
            {
                self = new Airport();
            }
            return self;
        }

        private void CreateStations()
        {
            Station station1 = new Station(1);
            Station station2 = new Station(2);
            Station station3 = new Station(3);
            Station station4 = new Station(4);
            Station station5 = new Station(5);
            Station station6 = new Station(6);
            Station station7 = new Station(7);
            Station station8 = new Station(8);
            
            station6.NextFlyingStations = new List<Station>() { station8 };
            station7.NextFlyingStations = new List<Station>() { station8 };
            station8.NextFlyingStations = new List<Station>() { station4 };

            station1.NextLandingStations = new List<Station>() { station2 };
            station2.NextLandingStations = new List<Station>() { station3 };
            station3.NextLandingStations = new List<Station>() { station4 };
            station4.NextLandingStations = new List<Station>() { station5 };
            station5.NextLandingStations = new List<Station>() { station6, station7 };

            AllStations = new Dictionary<int, Station>
            {
                { station1.ID, station1 },
                { station2.ID, station2 },
                { station3.ID, station3 },
                { station4.ID, station4 },
                { station5.ID, station5 },
                { station6.ID, station6 },
                { station7.ID, station7 },
                { station8.ID, station8 }
            };

            FirstLandingStations = new List<Station>() { station1 };
            FirstFlyingStations = new List<Station>() { station6, station7 };
            LastLandingStations = new List<Station>() { station6, station7 };
            LastFlyingStations = new List<Station>() { station4 };
        }
    }
}
