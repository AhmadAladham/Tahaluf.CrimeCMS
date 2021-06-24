using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class AllCriminalsDTO
    {
        public int CriminalId { get; set; }
        public int CrimeId { get; set; }
        public int CriminalNationalNumber { get; set; }
        public string CriminalFirstName { get; set; }
        public string? CriminalLastName { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string? Image { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string CrimeTtile { get; set; }
        public DateTime CrimeDate { get; set; }
        public string CrimeDescription { get; set; }
    }
}
