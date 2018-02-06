using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    [DataContract]
    public class StationDTO : INotifyPropertyChanged
    {
        [DataMember]
        public int StationID { get; set; }
        [DataMember]
        public int FlightID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
