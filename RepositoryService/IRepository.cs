﻿using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AirportRepositoryService
{
    [ServiceContract]
    public interface IRepository
    {
        [OperationContract]
        List<FlightDTO> GetFutureFlights();
        [OperationContract]
        FlightDTO AddFlightToSchedule(FlightDTO flight);
        [OperationContract]
        void AddFlightInfoToHistory(FlightInfoDTO flightInfo);
        [OperationContract]
        void UpdateStationState(StationDTO stationDTO);
        
    }
}
