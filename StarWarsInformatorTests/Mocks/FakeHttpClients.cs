using Moq;
using Moq.Protected;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StarWarsInformatorTests.Mocks
{
    public static class FakeHttpClients
    {
        public static HttpClient GetFakeClientThatThrowsException()
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .Throws(new HttpRequestException());

            return new HttpClient(handlerMock.Object);
        }

        public static HttpClient GetFakeClientThatReturnsData(SampleFile file)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StreamContent(File.OpenRead($"SampleData\\{file}.json")) };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            return new HttpClient(handlerMock.Object);
        }

        public enum SampleFile
        {
            SampleFilm, SamplePerson, SampleShip, SampleVehicle, SampleErrorData
        }
    }
}
