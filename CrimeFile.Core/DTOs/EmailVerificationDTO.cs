using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class EmailVerificationDTO
    {
        public string Email { get; set; }
        public int Code { get; set; }
    }
}
