using System;
namespace SpaceZAPI.Models
{
    public class Telemetry
    {
        public string id { get; set; }
        public string altitude { get; set; } = String.Empty;
        public string longitude { get; set; } = String.Empty;
        public string latitude { get; set; } = String.Empty;
        public string temperature { get; set; } = String.Empty;
        public double timeToOrbit { get; set; } 
    }
}

