using log4net;
using Newtonsoft.Json;
using SwApiClient.Model;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SwApiClient
{
    public class SwApiClient
    {
        ILog _logger;
        private readonly HttpClient _httpClient;
        private string _baseUri;

        public SwApiClient (ILog logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }
         
        private async Task<T> GetEntity<T>(string url)
        {
            try
            {
                _logger.InfoFormat("Trying to get data from {0}", url);
                using (var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        var message = string.Format("An error occured when trying to get data from {0}. Returned status code: {1}", url, response.StatusCode);
                        _logger.Error(message);
                        throw new ConnectionErrorException(message);
                    }

                    _logger.InfoFormat("Successfully downloaded data from {0}", url);
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        try
                        {
                            var serializer = new JsonSerializer();
                            serializer.ReferenceResolver = new ReferenceResolver();
                            var person = serializer.Deserialize<T>(new JsonTextReader(new StreamReader(stream)));

                            return person;
                        }
                        catch(Exception ex)
                        {
                            var message = string.Format("Data returned from {0} has invalid format.", url);
                            _logger.Error(message);
                            throw new DataParsingException(message, ex);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.ErrorFormat("HttpClient exception: {0}", ex.Message);
                throw new ConnectionErrorException("An error occured when trying to connect.", ex);
            }
        }

        /// <summary>
        /// Returns person identified by URL
        /// </summary>
        public Task<Person> GetPerson(string url)
            => GetEntity<Person>(url);

        /// <summary>
        /// Returns vehicle identified by URL
        /// </summary>
        public Task<Vehicle> GetVehicle(string url)
            => GetEntity<Vehicle>(url);

            
        /// <summary>
        /// Returns film identified by URL
        /// </summary>
        public Task<Film> GetFilm(string url)
            => GetEntity<Film>(url);

            
        /// <summary>
        /// Returns Ship identified by URL
        /// </summary>
        public Task<Ship> GetShip(string url)
            => GetEntity<Ship>(url);
    }
}
