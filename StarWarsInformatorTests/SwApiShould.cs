
using log4net;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using StarWarsInformatorTests.Mocks;
using SwApiClient;
using SwApiClient.Model;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StarWarsInformatorTests
{
    class SwApiShould
    {
        [Test]
        public void create_without_error()
        {
            var logMock = Mock.Of<ILog>();
            var httpClientMock = Mock.Of<HttpClient>();

            new SwApiClient.SwApiClient(logMock, httpClientMock);

            Assert.Pass();
        }

        [TestCase("Trying to get data from {0}", "foo")]
        public void log_information_when_trying_to_get_data(string expectedMessage, string uri)
        {
            var logMock = new Mock<ILog>();

            var httpClientMock = Mock.Of<HttpClient>();

            var client = new SwApiClient.SwApiClient(logMock.Object, httpClientMock);
            try
            {
                client.GetPerson(uri).Wait();
            }
            catch { }

            Assert.AreEqual(logMock.Invocations[0].Arguments[0], expectedMessage);
            Assert.AreEqual(logMock.Invocations[0].Arguments[1], uri);
        }

        [Test]
        public void throw_exception_on_connection_error()
        {
            var logMock = new Mock<ILog>();
            var clientMock = FakeHttpClients.GetFakeClientThatThrowsException();

            var client = new SwApiClient.SwApiClient(logMock.Object, clientMock);

            Assert.ThrowsAsync<ConnectionErrorException>(() => client.GetPerson("http://sampleAddress/something/1/"));
        }


        [Test]
        public void return_correct_person_object()
        {
            var logMock = new Mock<ILog>();
            var clientMock = FakeHttpClients.GetFakeClientThatReturnsData(FakeHttpClients.SampleFile.SamplePerson);

            var client = new SwApiClient.SwApiClient(logMock.Object, clientMock);

            var result = client.GetPerson("http://sampleAddress/something/1/").Result;

            Assert.IsNotNull(result);
            Assert.True(result is Person);
        }

        [Test]
        public void return_correct_vehicle_object()
        {
            var logMock = new Mock<ILog>();
            var clientMock = FakeHttpClients.GetFakeClientThatReturnsData(FakeHttpClients.SampleFile.SampleVehicle);

            var client = new SwApiClient.SwApiClient(logMock.Object, clientMock);

            var result = client.GetVehicle("http://sampleAddress/something/1/").Result;

            Assert.IsNotNull(result);
            Assert.True(result is Vehicle);
        }


        [Test]
        public void return_correct_ship_object()
        {
            var logMock = new Mock<ILog>();
            var clientMock = FakeHttpClients.GetFakeClientThatReturnsData(FakeHttpClients.SampleFile.SampleShip);

            var client = new SwApiClient.SwApiClient(logMock.Object, clientMock);

            var result = client.GetShip("http://sampleAddress/something/1/").Result;

            Assert.IsNotNull(result);
            Assert.True(result is Ship);
        }


        [Test]
        public void return_correct_film_object()
        {
            var logMock = new Mock<ILog>();
            var clientMock = FakeHttpClients.GetFakeClientThatReturnsData(FakeHttpClients.SampleFile.SampleVehicle);

            var client = new SwApiClient.SwApiClient(logMock.Object, clientMock);

            var result = client.GetFilm("http://sampleAddress/something/1/").Result;

            Assert.IsNotNull(result);
            Assert.True(result is Film);
        }
    }
}
