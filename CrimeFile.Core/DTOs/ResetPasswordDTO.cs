using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public int Code { get; set; }
        public string NewPassword { get; set; }
    }
}
