using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorExperience.ClientApp.Settings
{
    public class AppSettings
    {
        public const string CONFIG_KEY = "app";

        public string? ApiAddress { get; set; }
    }
}
