using AirportGUI.LogicServiceReference;
using AirportGUI.ViewModel;
using DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportGUI
{
    class CallbackHandler : IAirportManagerCallback
    {
        private MainViewModel mainViewModel;
        public CallbackHandler(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        //public void FlightAdd(FlightDTO flightDTO)
        //{
        //    if (mainViewModel.ListsInitialized)
        //    {
        //        mainViewModel.FutureFlights.Add(flightDTO);
        //        mainViewModel.UpdateFutureFlights();
        //    }
        //}

        //public void FlightRemove(FlightDTO flightDTO)
        //{
        //    if (mainViewModel.ListsInitialized)
        //    {
        //        mainViewModel.FutureFlights.Remove(flightDTO);
        //        mainViewModel.UpdateFutureFlights();
        //    }
        //}

        //public void StationStateUpdate(StationDTO stationDTO)
        //{
        //    if (mainViewModel.ListsInitialized)
        //    {
        //        mainViewModel.StationsState.Where(s => s.StationID == stationDTO.StationID).FirstOrDefault().FlightID = stationDTO.FlightID;
        //        mainViewModel.UpdateStationState();
        //    }
        //}

        public void FlightAdd(FlightDTO flightDTO)
        {
            mainViewModel.FlightAdd(flightDTO);
        }

        public void FlightRemove(FlightDTO flightDTO)
        {
            mainViewModel.FlightRemove(flightDTO);
        }

        public void StationStateUpdate(StationDTO stationDTO)
        {
            mainViewModel.StationStateUpdate(stationDTO);
        }
    }
}
