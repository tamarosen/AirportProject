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

namespace AirportGUI.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        InstanceContext instanceContext;
        AirportManagerClient client;
        public ObservableCollection<FlightDTO> FutureFlights { get; set; }
        public ObservableCollection<StationDTO> StationsState { get; set; }
        public MainViewModel()
        {
            instanceContext = new InstanceContext(new CallbackHandler(this));
            client = new AirportManagerClient(instanceContext);
            LoadFutureFlight();
            LoadStationsState();
        }

        public void LoadStationsState()
        {
            StationDTO[] stations = client.GetStationsState();
            StationsState = new ObservableCollection<StationDTO>(stations);
            RaisePropertyChanged(nameof(StationsState));
        }

        public void LoadFutureFlight()
        {
            FlightDTO[] futureFlights = client.GetFutureFlights();
            FutureFlights = new ObservableCollection<FlightDTO>(futureFlights);
            RaisePropertyChanged(nameof(FutureFlights));
        }
    }
}
