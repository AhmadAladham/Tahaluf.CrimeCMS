using CrimeFile.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class SearchCriminalsDTO : QueryStringParameters
    {
        public string? CriminalName { get; set; }
        public decimal? HeightFrom { get; set; }
        public decimal? HeightTo { get; set; }
        public decimal? WeightFrom { get; set; }
        public decimal? WeightTo { get; set; }
        public string SortingColumn { get; set; } = "Name";
        public string SortType { get; set; } = "ASC";
    }
}
