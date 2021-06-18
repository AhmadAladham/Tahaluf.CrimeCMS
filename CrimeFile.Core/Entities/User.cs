using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
        public string PasswordHash{ get; set; }
        public char Gender { get; set; }
        public bool EmailIsConfirmed { get; set; }
        public UserRole Role { get; set; }
    }
}
