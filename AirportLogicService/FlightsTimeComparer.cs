//using AirportLogicService.RepoServiceReference;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportLogicService
{
    class FlightsTimeComparer : IComparer<FlightDTO>
    {
        public int Compare(FlightDTO x, FlightDTO y)
        {
            return x.StartRouteTime.CompareTo(y.StartRouteTime);
        }
    }
}
