using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.Entities
{
    public class Crime
    {
        public int CrimeId { get; set; }
        public int StationId { get; set; }
        public int CrimeCategoryId { get; set; }
        public int? CriminalId { get; set; }
        public string CrimeTtile { get; set; }
        public DateTime? CrimeEntryDate { get; set; }
        public DateTime CrimeDate { get; set; }
        public bool IsClosed { get; set; }
        public string Location { get; set; }
        public string? CrimeDescription { get; set; }
        public DateTime? CloseDate { get; set; }
        public string? CriminalDescription { get; set; }
        public string? Image { get; set; }
    }
}
