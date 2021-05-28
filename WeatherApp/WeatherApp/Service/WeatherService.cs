using Android.Graphics;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Service
{
    public class WeatherService
    {
        const string ApiKey = "7c1bdd845b7edf384d50865dcfc920e7";

        public async Task<WeatherInfo> GetCityWeather(string city)
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={ApiKey}");
            var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(response);
            return weatherInfo;
        }

        public async Task<Bitmap> GetImageFromUrl(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        var bitmap = await BitmapFactory.DecodeStreamAsync(stream);
                        return bitmap;
                    }
                }
            }
            return null;
        }
    }
}