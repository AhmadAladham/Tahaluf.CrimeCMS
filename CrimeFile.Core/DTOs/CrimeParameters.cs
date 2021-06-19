using CrimeFile.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class CrimeParameters : QueryStringParameters
    {
        public string SortingColumn { get; set; } = "Title";
        public string SortType { get; set; } = "ASC";
    }
}
