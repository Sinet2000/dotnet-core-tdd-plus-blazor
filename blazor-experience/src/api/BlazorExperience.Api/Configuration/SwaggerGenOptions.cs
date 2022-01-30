using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorExperience.Api.Configuration
{
    public class SwaggerGenOptions
    {
        public const string CONFIG_KEY = "SwaggerGenOptions";

        public SwaggerDoc SwaggerDoc { get; set; }
        public SecurityDefinition SecurityDefinition { get; set; }
    }

    public class SwaggerDoc
    {
        public string Title { get; set; }
        public string Version { get; set; }
    }

    public class SecurityDefinition
    {
        public string Name { get; set; }

        public Authorisation Authorisation { get; set; }
    }

    public class Authorisation
    {
        public string Url { get; set; }
        public string TokenUrl { get; set; }

        public Dictionary<string, string> Scopes { get; set; }
    }
}
