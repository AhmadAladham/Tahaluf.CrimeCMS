using CrimeFile.Core.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrimeFile.Infra.Common
{
    public class ConfigManager : IConfigManager
    {
        private readonly IConfiguration _configuration;
        public ConfigManager(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string Key
        {
            get
            {
                return _configuration["AppSettings:Key"];
            }
        }


    }
}
