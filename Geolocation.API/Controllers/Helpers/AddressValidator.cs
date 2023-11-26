using FluentValidation;
using System.Net;
using System.Text.RegularExpressions;

namespace Geolocation.API.Controllers.Helpers
{
    public class AddressValidator : AbstractValidator<GeolocationRequest>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Address).NotNull().NotEmpty();
            RuleFor(x => x.Address).Must(AddressMustBeUriOrIp).WithMessage("Address must be in URL or IP format!!");
        }

        private bool AddressMustBeUriOrIp(string address) => AddressMustBeIp(address) || AddressMustBeUrl(address);

        private bool AddressMustBeIp(string ip)
        {
            IPAddress ipAddress;
            return IPAddress.TryParse(ip, out ipAddress);
        }

        private bool AddressMustBeUrl(string url)
        {
            string Pattern = @"-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(url);
        }
    }
}
