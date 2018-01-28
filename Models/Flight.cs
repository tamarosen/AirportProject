using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum State
    {
        Landing,
        TakingOff
    }
    public class Flight
    {
        public int ID { get; set; }
        public State State { get; set; }
        public Station CurrentStation { get; set; }
        public DateTime StartRouteTime { get; set; }
    }
}
