using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Android.Views.Animations;
using Newtonsoft.Json;


namespace StarwarsApp.Services
{
    public class RemoteDataService
    {
        public async Task<People> GetStarwarsPeople()
        {
            var client = new HttpClient();
            var response = await client
                .GetStringAsync("https://swapi.dev/api/people/");
            People data = null;

            data = response != null
                ? JsonConvert.DeserializeObject<People>(response)
                : null;
            return data;
        }

        public async Task<Films> GetStarwarsFilms()
        {
            var client = new HttpClient();
            var response = await client
                .GetStringAsync("https://swapi.dev/api/films");
            Films data = null;

            data = response != null
                ? JsonConvert.DeserializeObject<Films>(response)
                : null;
            return data;
        }
    }
}