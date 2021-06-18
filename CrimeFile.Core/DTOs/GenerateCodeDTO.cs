using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class GenerateCodeDTO
    {
        public int UserId { get; set; }
        public int CodeGenerated { get; set; }
        public string Email { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
