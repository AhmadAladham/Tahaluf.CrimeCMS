using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class AllComplaintsDTO
    {
        public int ComplaintId { get; set; }
        public string ComplaintTitle { get; set; }
        public DateTime ExpectedCrimeDate { get; set; }
        public DateTime ComplaintDate { get; set; }
        public string ComplaintDescription { get; set; }
        public string CriminalDescription { get; set; }
        public bool ComplaintStatus { get; set; }
        public string CrimeLocation { get; set; }
        public string Image { get; set; }
        public int CrimeCategoryId { get; set; }
        public string CrimeCategoryName { get; set; }
        public int StationId { get; set; }
        public string StationName { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
