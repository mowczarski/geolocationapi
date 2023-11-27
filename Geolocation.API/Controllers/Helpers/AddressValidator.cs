using FluentValidation;
using System;
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
            var tryCreate = assertUrl(url);

            if (!tryCreate && Regex.IsMatch(url ?? "", @"^www.", RegexOptions.IgnoreCase))
            {
                url = "https://" + url;
                return assertUrl(url);
            }

            return tryCreate;

            bool assertUrl(string url)
            {
                Uri? uriResult;
                return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }
        }
    }
}
