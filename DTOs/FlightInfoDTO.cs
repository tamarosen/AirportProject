using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    [DataContract]
    public class FlightInfoDTO
    {
        [DataMember]
        public int FlightID { get; set; }
        [DataMember]
        public int StationID { get; set; }
        [DataMember]
        public DateTime EnterTime { get; set; }
        [DataMember]
        public DateTime ExitTime { get; set; }
    }
}
