namespace Geolocation.Infrastructure.Clients.ApiStack
{
    public class ApiStackConfiguration : IApiStackConfiguration
    {
        public string Address { get; set; }
        public string ApiKey { get; set; }
    }

    public interface IApiStackConfiguration
    {
        string Address { get; set; }
        string ApiKey { get; set; }
    }
}