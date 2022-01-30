using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorExperience.Api.Configuration
{
    public class AuthSettings
    {
        public const string CONFIG_KEY = "AuthSettings";

        public JwtBearerOptions JwtBearerOptions { get; set; }
    }

    public class JwtBearerOptions
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
    }
}
