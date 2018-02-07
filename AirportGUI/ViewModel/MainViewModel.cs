using AirportGUI.LogicServiceReference;
using DTOs;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AirportGUI.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        InstanceContext instanceContext;
        AirportManagerClient client;
        Dispatcher dispatcher;
        public bool ListsInitialized { get; private set; }
        public TrulyObservableCollection<FlightDTO> FutureFlights { get; set; }
        public TrulyObservableCollection<StationDTO> StationsState { get; set; }
        public MainViewModel()
        {
            ListsInitialized = false;
            instanceContext = new InstanceContext(new CallbackHandler(this));
            client = new AirportManagerClient(instanceContext);
            dispatcher = Application.Current.Dispatcher;
            LoadFutureFlights();
            LoadStationsState();
            ListsInitialized = true;
        }

        public void LoadStationsState()
        {
            StationDTO[] stations = client.GetStationsState();
            StationsState = new TrulyObservableCollection<StationDTO>(stations);
            RaisePropertyChanged(nameof(StationsState));
        }

        public void LoadFutureFlights()
        {
            FlightDTO[] futureFlights = client.GetFutureFlights();
            FutureFlights = new TrulyObservableCollection<FlightDTO>(futureFlights);
            RaisePropertyChanged(nameof(FutureFlights));
        }

        public void FlightAdd(FlightDTO flightDTO)
        {
            if (ListsInitialized)
            {
                FutureFlights.Add(flightDTO);
                UpdateFutureFlights();
            }
        }

        public void FlightRemove(FlightDTO flightDTO)
        {
            if (ListsInitialized)
            {
                FlightDTO flt = FutureFlights.Where(f => f.StartRouteTime == flightDTO.StartRouteTime).FirstOrDefault();
                FutureFlights.Remove(flt);
                UpdateFutureFlights();
            }
        }

        public void StationStateUpdate(StationDTO stationDTO)
        {
            if (ListsInitialized)
            {
                StationDTO st = StationsState.Where(s => s.StationID == stationDTO.StationID).FirstOrDefault();
                StationsState.Remove(st);
                StationsState.Insert(st.StationID - 1, stationDTO);
                //StationsState.Add(stationDTO);
                UpdateStationState();
            }
        }

        public void UpdateFutureFlights()
        {
            dispatcher.Invoke(() =>
                RaisePropertyChanged(nameof(FutureFlights)));
        }
        public void UpdateStationState()
        {
            dispatcher.Invoke(() =>
                RaisePropertyChanged(nameof(StationsState)));
        }
    }
}
