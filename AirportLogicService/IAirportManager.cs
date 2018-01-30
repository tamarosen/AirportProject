using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using AirportLogicService.RepoServiceReference;
using System.Threading.Tasks;
using AirportLogicService.ContractTypes;

namespace AirportLogicService
{
    [ServiceContract]
    public interface IAirportManager
    {
        [OperationContract]
        bool ScheduleNewFlight(DTOs.FlightDTO flightDTO);
    }
}
