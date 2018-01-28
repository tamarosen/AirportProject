using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public enum State
    {
        Landing,
        TakingOff
    }
    [DataContract]
    public class FlightDTO
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public State State { get; set; }
        [DataMember]
        public DateTime StartRouteTime { get; set; }
    }
}
