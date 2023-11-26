namespace Geolocation.API.Controllers.Helpers
{
    public static class Endpoints
    {
        public const string RootPath = "api";
        public const string Test = $"{RootPath}/testconnection";
        public const string Healthcheck = $"{RootPath}/healthcheck";

        public const string Geolocation = $"{RootPath}/geolocation";
        public const string GetGeolocation = "{address}";
        public const string DeleteGeolocation = "remove";
        public const string AddGeolocation = "add";
    }
}
