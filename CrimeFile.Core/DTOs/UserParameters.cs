using CrimeFile.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class UserParameters : QueryStringParameters
    {
        public string SortingColumn { get; set; } = "Name";
        public string SortType { get; set; } = "ASC";
    }
}
