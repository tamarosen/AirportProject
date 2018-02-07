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

            managerClient = new AirportManagerClient(new System.ServiceModel.InstanceContext(new Callback()));

            Timer timer = new Timer(CreateNewFlight, null, 0, 3000);

            Console.ReadLine();

            managerClient.Close();
            
        }

        private static void CreateNewFlight(object state)
        {
            FlightDTO newFlight = new FlightDTO()
            {
                State = (State)Enum.GetValues(typeof(State)).GetValue(rnd.Next(0,2)), // upper bound exclusive!
                StartRouteTime = DateTime.Now.AddMilliseconds(rnd.Next(5000, 100000) + 100000)
            };

            managerClient.ScheduleNewFlight(newFlight);

            Console.WriteLine($"State: {newFlight.State}\t Time: {newFlight.StartRouteTime}\n");
        }

        public class Callback : IAirportManagerCallback
        {
            public void FlightAdd(FlightDTO flightDTO) { }

            public void FlightRemove(FlightDTO flightDTO) { }
            
            public void StationStateUpdate(StationDTO stationDTO) { }
        }
    }
}
