using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using AirportLogicService.RepositoryServiceReference;
using System.Threading.Tasks;
using AirportLogicService.ContractTypes;

namespace AirportLogicService
{
    [ServiceContract(SessionMode= SessionMode.Required, CallbackContract = typeof(IAirportDuplexCallback))]
    public interface IAirportManager
    {
        [OperationContract]
        bool ScheduleNewFlight(DTOs.FlightDTO flightDTO);
        [OperationContract]
        IEnumerable<DTOs.FlightDTO> GetFutureFlights();
        [OperationContract]
        IEnumerable<DTOs.StationDTO> GetStationsState();
    }

    public interface IAirportDuplexCallback
    {
        [OperationContract(IsOneWay = true)]
        void FlightUpdate();
        [OperationContract(IsOneWay = true)]
        void StationStateUpdate();
    }
}
