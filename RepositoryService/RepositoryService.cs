using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using RepositoryService;

namespace AirportRepositoryService
{
    public class RepositoryService : IRepository
    {
        AirportDBEntities1 db;
        public RepositoryService()
        {
            db = new AirportDBEntities1();
        }

        public void AddFlightInfoToHistory(FlightInfoDTO flightInfo)
        {
            db.FlightHistories.Add(new FlightHistory()
            {
                FlightID = flightInfo.FlightID,
                StationID = flightInfo.StationID,
                EnterTime = flightInfo.EnterTime,
                ExitTime = flightInfo.ExitTime
            });
        }

        public FlightDTO AddFlightToSchedule(FlightDTO flight)
        {
            try
            {
                // int maxId = Convert.ToInt32(db.Database.SqlQuery<decimal>("Select IDENT_CURRENT ('Schedule')", new object[0]).FirstOrDefault());

                Schedule schedule = db.Schedules.Add(new Schedule()
                {
                    // FlightID = maxId + 1,
                    StartRouteTime = flight.StartRouteTime,
                    State = (flight.State).ToString()
                });

                db.SaveChanges();

                flight.ID = schedule.FlightID;
                return flight;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<FlightDTO> GetFutureFlights()
        {
            List<FlightDTO> list = new List<FlightDTO>();

            DbSet<Schedule> schedules = db.Schedules;
            
            foreach (var flight in schedules)
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

        public void UpdateStationState(StationDTO stationDTO)
        {
            db.StationStates.Where(s => s.StationID == stationDTO.StationID).FirstOrDefault().FlightID = stationDTO.FlightID;                        
        }
    }
}
