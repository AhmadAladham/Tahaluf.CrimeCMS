using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class ChangePasswordDTO
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
      
    }
}
