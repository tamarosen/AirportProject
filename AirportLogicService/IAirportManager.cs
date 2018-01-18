using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AirportLogicService
{
    [ServiceContract]
    public interface IAirportManager
    {
        [OperationContract]
        void StartLanding(Flight flight);
        [OperationContract]
        void StartTakingOff(Flight flight);
        [OperationContract]
        void MoveToNextStation(Flight flight);
    }
}
