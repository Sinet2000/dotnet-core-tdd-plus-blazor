using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorExperience.Api.Configuration
{
    public class ApiSettings
    {
        public const string CONFIG_KEY = "ApiSettings";

        public string[] ClientAppOrigins { get; set; }
    }
}
