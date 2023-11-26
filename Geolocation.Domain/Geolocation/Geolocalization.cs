using System;

namespace Geolocation.Domain
{
    public class Geolocalization : EntityBase<int>
    {
        public AddressType Type { get; private set; }
        public string Ip { get; private set; }
        public string? Url { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string Continent { get; private set; }
        public string Country { get; private set; }
        public string? Region { get; private set; }
        public string? City { get; private set; }
        public string? CityCode { get; private set; }
        public virtual Location Location { get; private set; }

        public Geolocalization(
            string url,
            string type,
            string ip,
            double latitude,
            double longitude,
            string continent,
            string country,
            string? region,
            string? city,
            string? cityCode,
            Location location) : base()
        {
            Type = Enum.Parse<AddressType>(type);
            Ip = ip;
            Url = url;
            Latitude = latitude;
            Longitude = longitude;
            Continent = continent;
            Country = country;
            Region = region;
            City = city;
            CityCode = cityCode;
            Location = location;
        }

        public Geolocalization() { }
    }
}
