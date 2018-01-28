using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace AirportRepositoryService
{
    public class RepositoryService : IRepository
    {
        AirportDBEntities db;
        public RepositoryService()
        {
            db = new AirportDBEntities();
        }
        public bool AddFlightToScedule(FlightDTO flight)
        {
            try
            {
                db.Scedules.Add(new Scedule()
                {
                    FlightID = flight.ID,
                    StartRouteTime = flight.StartRouteTime,
                    State = (flight.State).ToString()
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<FlightDTO> GetFutureFlights()
        {
            List<FlightDTO> list = new List<FlightDTO>();
            foreach (var flight in db.Scedules)
            {
                FlightDTO flightDTO = new FlightDTO()
                {
                    ID = flight.FlightID,
                    StartRouteTime = flight.StartRouteTime,
                    State = (State)Enum.Parse(typeof(State), ((flight.State).ToString()))
                };
                if (flight.StartRouteTime > DateTime.Now)
                    list.Add(flightDTO);
            }
            return list;
        }
    }
}
