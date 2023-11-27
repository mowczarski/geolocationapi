using FluentAssertions;
using Geolocation.Application;
using Geolocation.Domain;
using Geolocation.Domain.Abstrations;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Test.Integration
{
    [TestFixture(Category = "Integration")]
    public class GetGeolocationQueryTest
    {
        private Mock<IGeolocalizationRepository> _geolocalizationRepository;
    
        [SetUp]
        public void Setup()
        {
            _geolocalizationRepository = new();
        }

        [Test]
        public async Task Handle_Should_Get_Data()
        {
            _geolocalizationRepository.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new Geolocalization());

            var query = new GetGeolocationQuery("https://github.com");

            var handler = new GetGeolocationQueryHandler(_geolocalizationRepository.Object);

            var result = await handler.Handle(query, default);

            result.IsSuccess.Should().BeTrue();
            _geolocalizationRepository.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        }

        [Test]
        public async Task Handle_Not_Get_Data_When_Geolocation_Is_Missing()
        {
            _geolocalizationRepository.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);

            var query = new GetGeolocationQuery("https://github.com");

            var handler = new GetGeolocationQueryHandler(_geolocalizationRepository.Object);

            var result = await handler.Handle(query, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(GeolocationErrors.NotFound);
            _geolocalizationRepository.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        }
    }
}
