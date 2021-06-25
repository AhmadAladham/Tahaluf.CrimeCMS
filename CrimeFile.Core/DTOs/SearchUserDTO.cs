using CrimeFile.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.DTOs
{
    public class SearchUserDTO : QueryStringParameters
    {
        public int? RoleId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string SortingColumn { get; set; } = "Name";
        public string SortType { get; set; } = "ASC";
    }
}
