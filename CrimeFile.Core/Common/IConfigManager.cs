using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Core.Common
{
    public interface IConfigManager
    {
        public string Key { get; }
        public string CompanyEmail { get; }
        public string CompanyEmailPassword { get; }
    }
}
