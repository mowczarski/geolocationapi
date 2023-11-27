namespace Geolocation.Application 
{ 
    public sealed class GetGeolocationResponse
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string Continent { get; private set; }
        public string Country { get; private set; }
        public string? Region { get; private set; }
        public string? City { get; private set; }
        public string? CityCode { get; private set; }
       
        public GetGeolocationResponse(
            double latitude,
            double longitude, 
            string continent, 
            string country,
            string? region,
            string? city,
            string? cityCode)
        {
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