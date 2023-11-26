namespace GeolocationAPI.Models
{
    public static class Endpoints
    {
        public const string RootPath = "api";
        public const string Test = $"{RootPath}/testconnection";

        public const string Geolocation = $"{RootPath}/geolocation";
        public const string GetGeolocation = "{ipAddress}";
        public const string GetGeolocationFromDb = "db/{ipAddress}";
    }
}
