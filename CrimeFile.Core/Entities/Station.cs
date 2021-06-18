using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.Entities
{
    public class Station
    {
        public int StationId { get; set; }
        public string StationName { get; set; }
        public string PhoneNumber { get; set; }
        public string StationAddress { get; set; }
        public int TotalStaff { get; set; }
    }
}
