using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportLogicService
{
    class CallbacksHolder
    {
        public static CallbacksHolder self;
        private List<IAirportDuplexCallback> callbacks;

        private CallbacksHolder()
        {
            callbacks = new List<IAirportDuplexCallback>();
        }

        public static CallbacksHolder Instance()
        {
            if (self == null)
            {
                self = new CallbacksHolder();
            }
            return self;
        }

        public void Add(IAirportDuplexCallback cb)
        {
            lock (this)
            {
                callbacks.Add(cb);
            }
        }

        public void Remove(IAirportDuplexCallback cb)
        {
            lock (this)
            {
                callbacks.Remove(cb);
            }
        }

        public void FlightAdd(DTOs.FlightDTO flightDTO)
        {
            lock (this)
            {
                foreach (var cb in callbacks)
                {
                    try
                    {
                        cb.FlightAdd(flightDTO);
                    }
                    catch (Exception)
                    {
                        callbacks.Remove(cb);
                    }
                }
            }
        }

        public void FlightRemove(DTOs.FlightDTO flightDTO)
        {
            lock (this)
            {
                foreach (var cb in callbacks)
                {
                    try
                    {
                        cb.FlightRemove(flightDTO);
                    }
                    catch (Exception)
                    {
                        callbacks.Remove(cb);
                    }
                }
            }
        }
        public void StationStateUpdate(Station station)
        {
            DTOs.StationDTO st = new DTOs.StationDTO() { StationID = station.ID, FlightID = station.GetFlightInStation() };
            lock (this)
            {
                foreach (var cb in callbacks)
                {
                    try
                    {
                        cb.StationStateUpdate(st);
                    }
                    catch (Exception)
                    {
                        callbacks.Remove(cb);
                    }
                }
            }
        }
    }
}
