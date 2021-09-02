
using log4net;
using Moq;
using NUnit.Framework;
using SwApiClient;
using System.Net.Http;

namespace StarWarsInformatorTests
{
    class SwApiShould
    {
        private ILog logMock;


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

            var httpClientMock = new Mock<HttpClient>();

            httpClientMock.Setup(client => client.GetStreamAsync("foo")).Throws(new HttpRequestException());

            var client = new SwApiClient.SwApiClient(logMock.Object, httpClientMock.Object);
            try
            {
                client.GetPerson("foo").Wait();
            }
            catch (ConnectionErrorException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

    }
}
