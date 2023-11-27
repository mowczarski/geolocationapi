using FluentAssertions;
using Geolocation.Application;
using Geolocation.Domain;
using Geolocation.Domain.Abstrations;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Test.Integration
{
    [TestFixture(Category = "Integration")]
    public class AddGeolocationCommandTest
    {
        private Mock<IGeolocalizationRepository> _geolocalizationRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IApiStackClient> _apiStackClient;

        private ApiStackResponse apiStackResponse = new ApiStackResponse
        {
            ip = "127.0.0.1",
            type = "ipv4",
            continent_name = "Europe",
            country_name = "Poland",
            latitude = 51.456876545d,
            longitude = -14.45678733,
            location = new ApiStackLocation
            {
                capital = "Warsaw",
                country_flag = "htpp://immg.pl",
                calling_code = "48",
                is_eu = true,
                languages = new List<ApiStackLanguage>()
                {
                    new ApiStackLanguage
                    {
                        code = "pl",
                        name = "Polish",
                        native = "Poliski"
                    }
                }
            }
        };

        [SetUp]
        public void Setup()
        {
            _geolocalizationRepository = new();
            _unitOfWork = new();
            _apiStackClient = new();
        }

        [Test]
        public async Task Handle_Should_Add_Data()
        {
            _geolocalizationRepository.Setup(x => x.AddAsync(It.IsAny<Geolocalization>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _unitOfWork.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(() => 1);
            _apiStackClient.Setup(x => x.GetGeolocationDataAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => apiStackResponse);

            var command = new AddGeolocationCommand("127.0.0.1");

            var handler = new AddGeolocationCommandHandler(_apiStackClient.Object, _geolocalizationRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(command, default);

             result.IsSuccess.Should().BeTrue();
            _geolocalizationRepository.Verify(x => x.AddAsync(It.IsAny<Geolocalization>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
            _unitOfWork.Verify(x => x.SaveChangesAsync(default), Times.Exactly(1));
            _apiStackClient.Verify(x => x.GetGeolocationDataAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        }

        [Test]
        public async Task Handle_Should_Not_Add_Data_When_ApiStack_Not_Give_Response()
        {
            _geolocalizationRepository.Setup(x => x.AddAsync(It.IsAny<Geolocalization>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _unitOfWork.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(() => 1);
            _apiStackClient.Setup(x => x.GetGeolocationDataAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);

            var command = new AddGeolocationCommand("https://github.com");

            var handler = new AddGeolocationCommandHandler(_apiStackClient.Object, _geolocalizationRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(GeolocationErrors.ApiStackError);
            _geolocalizationRepository.Verify(x => x.AddAsync(It.IsAny<Geolocalization>(), It.IsAny<CancellationToken>()), Times.Exactly(0));
            _unitOfWork.Verify(x => x.SaveChangesAsync(default), Times.Exactly(0));
            _apiStackClient.Verify(x => x.GetGeolocationDataAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        }

        [Test]
        public async Task Handle_Should_Not_Add_Data_When_Duplicated()
        {
            _geolocalizationRepository.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
              .ReturnsAsync(() => new Geolocalization("https://github.com","ipv4","",0,0,"","","","","", null));

            var command = new AddGeolocationCommand("https://github.com");

            var handler = new AddGeolocationCommandHandler(_apiStackClient.Object, _geolocalizationRepository.Object, _unitOfWork.Object);

            var result = await handler.Handle(command, default);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(GeolocationErrors.DuplicateError);
            _geolocalizationRepository.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Exactly(1));;
        }
    }
}