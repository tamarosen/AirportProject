﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AirportGUI.LogicServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LogicServiceReference.IAirportManager", CallbackContract=typeof(AirportGUI.LogicServiceReference.IAirportManagerCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IAirportManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAirportManager/ScheduleNewFlight", ReplyAction="http://tempuri.org/IAirportManager/ScheduleNewFlightResponse")]
        bool ScheduleNewFlight(DTOs.FlightDTO flightDTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAirportManager/ScheduleNewFlight", ReplyAction="http://tempuri.org/IAirportManager/ScheduleNewFlightResponse")]
        System.Threading.Tasks.Task<bool> ScheduleNewFlightAsync(DTOs.FlightDTO flightDTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAirportManager/GetFutureFlights", ReplyAction="http://tempuri.org/IAirportManager/GetFutureFlightsResponse")]
        DTOs.FlightDTO[] GetFutureFlights();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAirportManager/GetFutureFlights", ReplyAction="http://tempuri.org/IAirportManager/GetFutureFlightsResponse")]
        System.Threading.Tasks.Task<DTOs.FlightDTO[]> GetFutureFlightsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAirportManager/GetStationsState", ReplyAction="http://tempuri.org/IAirportManager/GetStationsStateResponse")]
        DTOs.StationDTO[] GetStationsState();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAirportManager/GetStationsState", ReplyAction="http://tempuri.org/IAirportManager/GetStationsStateResponse")]
        System.Threading.Tasks.Task<DTOs.StationDTO[]> GetStationsStateAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAirportManagerCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAirportManager/FlightUpdate")]
        void FlightUpdate();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAirportManager/StationStateUpdate")]
        void StationStateUpdate();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAirportManagerChannel : AirportGUI.LogicServiceReference.IAirportManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AirportManagerClient : System.ServiceModel.DuplexClientBase<AirportGUI.LogicServiceReference.IAirportManager>, AirportGUI.LogicServiceReference.IAirportManager {
        
        public AirportManagerClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public AirportManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public AirportManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public AirportManagerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public AirportManagerClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public bool ScheduleNewFlight(DTOs.FlightDTO flightDTO) {
            return base.Channel.ScheduleNewFlight(flightDTO);
        }
        
        public System.Threading.Tasks.Task<bool> ScheduleNewFlightAsync(DTOs.FlightDTO flightDTO) {
            return base.Channel.ScheduleNewFlightAsync(flightDTO);
        }
        
        public DTOs.FlightDTO[] GetFutureFlights() {
            return base.Channel.GetFutureFlights();
        }
        
        public System.Threading.Tasks.Task<DTOs.FlightDTO[]> GetFutureFlightsAsync() {
            return base.Channel.GetFutureFlightsAsync();
        }
        
        public DTOs.StationDTO[] GetStationsState() {
            return base.Channel.GetStationsState();
        }
        
        public System.Threading.Tasks.Task<DTOs.StationDTO[]> GetStationsStateAsync() {
            return base.Channel.GetStationsStateAsync();
        }
    }
}
