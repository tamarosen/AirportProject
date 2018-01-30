using AirportLogicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace AirportLogicHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri logicBaseAddress = new Uri("http://localhost:8733/AirportLogicService/AirportManagerService/");

            ServiceHost logicSelfHost = new ServiceHost(typeof(AirportManagerService), logicBaseAddress);

            try
            {
                logicSelfHost.AddServiceEndpoint(typeof(IAirportManager), new WSHttpBinding(), "AirportManagerService");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                logicSelfHost.Description.Behaviors.Add(smb);

                logicSelfHost.Open();
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                logicSelfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                logicSelfHost.Abort();
            }
        }
    }
}
