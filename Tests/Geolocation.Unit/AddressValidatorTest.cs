using FluentValidation.TestHelper;
using Geolocation.API.Controllers.Helpers;
using NUnit.Framework;

namespace Geolocation.Test.Unit
{
    [TestFixture(Category = "Unit")]
    public class AddGeolocationCommandTest
    {
        private AddressValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new AddressValidator();
        }

        [Test]
        public void Should_Have_Error_When_Value_Is_Null()
        {
            var model = new GeolocationRequest { Address = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(request => request.Address);
        }

        [Test]
        public void Should_Have_Error_When_Value_Is_Empty()
        {
            var model = new GeolocationRequest { Address = "" };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(request => request.Address);
        }

        [TestCase("www.wp.com", true)]
        [TestCase("https://www.google.com/", true)]
        [TestCase("192.168.1.0", true)]
        [TestCase("2a01:7e01::f03c:94ff:fe0a:2832", true)]
        [TestCase("httdp://google.com/", false)]
        [TestCase("192,168,1,0", false)]
        [TestCase("abc", false)]
        [TestCase("149.145.1478.64588", false)]
        public void Should_Block_Invalid_Ip_And_Url(string value, bool shouldPass)
        {
            var model = new GeolocationRequest { Address = value };
            var result = validator.TestValidate(model);

            if (!shouldPass)
                result.ShouldHaveValidationErrorFor(request => request.Address);
            else
                result.ShouldNotHaveValidationErrorFor(request => request.Address);
        }

    }
}
