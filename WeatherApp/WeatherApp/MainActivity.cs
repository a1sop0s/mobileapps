using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using WeatherApp.Service;

namespace WeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var cityTextView = FindViewById<TextView>(Resource.Id.cityTextView);
            var tempTextView = FindViewById<TextView>(Resource.Id.tempTextView);
            var weatherImage = FindViewById<ImageView>(Resource.Id.weatherImage);

            var weatherService = new WeatherService();

            var weatherInfo = await weatherService.GetCityWeather("Tallinn");
            cityTextView.Text = weatherInfo.Name;
            tempTextView.Text = weatherInfo.Main.Temp.ToString();

            weatherImage.SetImageBitmap(await weatherService.GetImageFromUrl($"https://openweathermap.org/img/wn/{weatherInfo.Weather[0].Icon}@2x.png"));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}