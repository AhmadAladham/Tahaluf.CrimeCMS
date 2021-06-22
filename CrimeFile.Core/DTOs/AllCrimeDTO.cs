using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class AllCrimeDTO
    {
        public int CrimeId { get; set; }
        public string CrimeTtile { get; set; }
        public DateTime CrimeEntryDate { get; set; }
        public DateTime CrimeDate { get; set; }
        public bool IsClosed { get; set; }
        public string Location { get; set; }
        public string? CrimeDescription { get; set; }
        public DateTime? CloseDate { get; set; }
        public string CrimeCategoryName { get; set; }
        public string? CriminalFirstName { get; set; }
        public string? CriminalLastName { get; set; }
        public string StationName { get; set; }
        public string? Image { get; set; }
    }
}
