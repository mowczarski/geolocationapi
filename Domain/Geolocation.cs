using System.Collections.Generic;

namespace GeolocationAPI.Domain
{
    public class Geolocation : EntityBase<int>
    {
        public string Type { get; set; }
        public string Ip { get; set; }
        public string? Url { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public virtual List<Location> Locations { get; set;}
    }
}
