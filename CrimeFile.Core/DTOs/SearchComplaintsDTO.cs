using CrimeFile.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class SearchComplaintsDTO : QueryStringParameters
    {
        public string? ComplaintTitle { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? ComplaintStatus { get; set; }
        public int? CrimeCategoryId { get; set; }
        public int? StationId { get; set; }
        public string SortingColumn { get; set; } = "Title";
        public string SortType { get; set; } = "ASC";
    }
}
