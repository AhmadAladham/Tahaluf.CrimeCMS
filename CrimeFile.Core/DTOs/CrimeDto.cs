﻿using CrimeFile.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class CrimeDto:QueryStringParameters
    {
        public string? CrimeTtile { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Location { get; set; }
        public int? CrimeCategyId { get; set; }
        public int? StationId { get; set; }

    }
}
