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
    public class RemoveGeolocationCommandTest
    {
        private Mock<IGeolocalizationRepository> _geolocalizationRepository;
        private Mock<IUnitOfWork> _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _geolocalizationRepository = new();
            _unitOfWork = new();
        }

        [Test]
        public async Task Handle_Should_Remove_Geolocation()
        {
            _geolocalizationRepository.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new Geolocalization());
            _geolocalizationRepository.Setup(x => x.Remove(It.IsAny<Geolocalization>()));
            _unitOfWork.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(() => 1);

            var command = new RemoveGeolocationCommand("https://github.com");
            
            var handler = new RemoveGeolocationCommandHandler(_geolocalizationRepository.Object, _unitOfWork.Object);
            
            var result = await handler.Handle(command, default);

            result.IsSuccess.Should().BeTrue();
            _geolocalizationRepository.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
            _geolocalizationRepository.Verify(x => x.Remove(It.IsAny<Geolocalization>()), Times.Exactly(1));
            _unitOfWork.Verify(x => x.SaveChangesAsync(default), Times.Exactly(1));
        }

        [Test]
        public async Task Handle_Should_Not_Remove_When_Geolocation_Not_Found()
        {
            _geolocalizationRepository.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);
            _geolocalizationRepository.Setup(x => x.Remove(It.IsAny<Geolocalization>()));
            _unitOfWork.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(() => 1);

            var command = new RemoveGeolocationCommand("https://github.com");
            
            var handler = new RemoveGeolocationCommandHandler(_geolocalizationRepository.Object, _unitOfWork.Object);
            
            var result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(GeolocationErrors.CannotRemove);
            _geolocalizationRepository.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
            _geolocalizationRepository.Verify(x => x.Remove(It.IsAny<Geolocalization>()), Times.Exactly(0));
            _unitOfWork.Verify(x => x.SaveChangesAsync(default), Times.Exactly(0));
        }
    }
}
