using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class EditComplaintStatusDTO
    {
        public int ComplaintId { get; set; }
        public int ComplaintStatus { get; set; }
    }
}
