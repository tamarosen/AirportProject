using AirportSimulator.LogicServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AirportSimulator
{
    class Program
    {
        static AirportManagerClient managerClient;
        static Random rnd;
        static void Main(string[] args)
        {
            rnd = new Random();

            managerClient = new AirportManagerClient();

            Timer timer = new Timer(CreateNewFlight, null, 0, 10000);
            
        }

        private static void CreateNewFlight(object state)
        {
            FlightDTO newFlight = new FlightDTO()
            {
                State = (State)Enum.GetValues(typeof(State)).GetValue(rnd.Next(0,1)),
                StartRouteTime = DateTime.Now.AddMilliseconds(rnd.Next(5000, 10000))
            };

            managerClient.ScheduleNewFlight(newFlight);
        }
    }
}
