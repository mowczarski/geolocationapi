using Geolocation.Domain;

namespace Geolocation.Application 
{ 
    public sealed class GetGeolocationResponse
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
       
        public GetGeolocationResponse(
            AddressType type, 
            string ip, 
            string url,
            double latitude,
            double longitude, 
            string continent, 
            string country,
            string? region,
            string? city,
            string? cityCode)
        {
            Type = type;
            Ip = ip;
            Url = url;
            Latitude = latitude;
            Longitude = longitude;
            Continent = continent;
            Country = country;
            Region = region;
            City = city;
            CityCode = cityCode;
        }

        private GetGeolocationResponse()
        {
        }
    }
}