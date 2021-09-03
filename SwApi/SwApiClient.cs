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
         

        private async Task<T> GetEntity<T>(string url) where T : BaseSwEntity
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
                        return ParseJsonStream<T>(stream);
                    }
                }
            }
            catch(HttpRequestException ex)
            {
                _logger.ErrorFormat("HttpClient exception: {0}", ex.Message);
                throw new ConnectionErrorException("An error occured when trying to connect.", ex);
            }
        }

        public T ParseJsonStream<T>(Stream jsonStream) where T : BaseSwEntity
        {
            var serializer = new JsonSerializer();
            return serializer.Deserialize<T>(new JsonTextReader(new StreamReader(jsonStream)));
        }


        /// <summary>
        /// Returns person identified by URL
        /// </summary>
        public async Task<Person> GetPerson(string url)
            => await GetEntity<Person>(url);

        /// <summary>
        /// Returns vehicle identified by URL
        /// </summary>
        public async Task<Vehicle> GetVehicle(string url)
            => await GetEntity<Vehicle>(url);


        /// <summary>
        /// Returns film identified by URL
        /// </summary>
        public async Task<Film> GetFilm(string url)
            => await GetEntity<Film>(url);

            
        /// <summary>
        /// Returns Ship identified by URL
        /// </summary>
        public async Task<Ship> GetShip(string url)
            => await GetEntity<Ship>(url);
    }
}
