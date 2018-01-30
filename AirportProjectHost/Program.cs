using AirportRepositoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace AirportRepositoryHost
{
    class Program
    {
        static void Main(string[] args)
        {  
            Uri repositoryBaseAddress = new Uri("http://localhost:8734/RepositoryService/RepositoryService/");
            
            ServiceHost repositorySelfHost = new ServiceHost(typeof(AirportRepositoryService.RepositoryService), repositoryBaseAddress);

            try
            {
                repositorySelfHost.AddServiceEndpoint(typeof(IRepository), new WSHttpBinding(), "RepositoryService");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                repositorySelfHost.Description.Behaviors.Add(smb);
                
                repositorySelfHost.Open();
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();
                
                repositorySelfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                repositorySelfHost.Abort();
            }
        }
    }
}
