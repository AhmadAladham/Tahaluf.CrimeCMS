using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.Entities
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
