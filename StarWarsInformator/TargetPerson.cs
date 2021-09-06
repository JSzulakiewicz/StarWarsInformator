using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsInformator
{
    class TargetPerson
    {
        public TargetPerson(SwApiClient.SwApiClient client, string address)
        {
            var person = client.GetPerson(address).Result;
            this.name = person.Name;

            var filmsTask = getFilms(client, person.Films);
            var vehicleTask = getVehicles(client, person.Vehicles);
            var starshipTask = getShips(client, person.Starships);

            Task.WaitAll(filmsTask, vehicleTask, starshipTask);
        }

        private async Task getShips(SwApiClient.SwApiClient client, IEnumerable<string> starships)
        {
            var downloadTask = starships.Select(address => client.GetShip(address));
            Task.WaitAll(downloadTask.ToArray());
            this.ships = downloadTask.Select(task => task.Result.Name).ToArray();
        }

        private async Task getVehicles(SwApiClient.SwApiClient client, IEnumerable<string> vehicles)
        {
            var downloadTask = vehicles.Select(address => client.GetVehicle(address));
            Task.WaitAll(downloadTask.ToArray());
            this.vehicles = downloadTask.Select(task => task.Result.Name).ToArray();
        }

        private async Task getFilms(SwApiClient.SwApiClient client, IEnumerable<string> films)
        {
            var downloadTask = films.Select(address => client.GetFilm(address));
            Task.WaitAll(downloadTask.ToArray());
            this.films = downloadTask.Select(task => task.Result.Title).ToArray();
        }
        internal void ExportToJson(string filename)
        {
            var serializer = JsonSerializer.CreateDefault();
            using (var streamWriter = new StreamWriter(filename))
            using (var jsonWriter = new JsonTextWriter(streamWriter))
                serializer.Serialize(jsonWriter, this);
        }

        [JsonProperty]
        public string name { get; set; }

        [JsonProperty]
        public string[] films { get; set; }

        [JsonProperty]
        public string[] vehicles { get; set; }

        [JsonProperty]
        public string[] ships { get; set; }
    }
}
