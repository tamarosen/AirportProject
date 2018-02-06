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

        public void FlightUpdate()
        {
            mainViewModel.LoadFutureFlight();
        }

        public void StationStateUpdate()
        {
            mainViewModel.LoadStationsState();
        }
    }
}
