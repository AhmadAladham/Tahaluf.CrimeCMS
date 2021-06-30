using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class EditUserDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        
    }
}
