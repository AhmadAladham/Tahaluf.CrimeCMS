using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.Entities
{
    public class Complaint
    {
        public int ComplaintId { get; set; }
        public int UserId { get; set; }
        public int CrimeCategoryId { get; set; }
        public int StationId { get; set; }
        public string ComplaintTitle { get; set; }
        public DateTime ExpectedCrimeDate { get; set; }
        public DateTime ComplaintDate { get; set; }
        public string ComplaintDescription { get; set; }
        public string CriminalDescription { get; set; }
        public int ComplaintStatus { get; set; }
        public string CrimeLocation { get; set; }
        public string Image { get; set; }
    }
}
