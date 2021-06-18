using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.Entities
{
    public class Criminal
    {
        public int CriminalId { get; set; }
        public string CriminalFirstName { get; set; }
        public string CriminalLastName { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

    }
}
