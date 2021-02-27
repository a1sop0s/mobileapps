#nullable enable
using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using CarBrands.Models;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace CarBrands
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var fab = FindViewById<FloatingActionButton>(Resource.Id.toolbar);
            if (fab != null) fab.Click += fabOnClick;

            var items = new List<Car>
            {
                new Car {Manufacturer = "Ford", Model = "Focus", kW = 80}
            };

            var carListView = FindViewById<ListView>(Resource.Id.carListView);
            if (carListView != null) carListView.Adapter = new CarAdapter(this, items);
        }

        public override bool OnCreateOptionsMenu(IMenu? menu)
        {
            MenuInflater?.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem? item)
        {
            var id = item!.ItemId;
            if (id == Resource.Id.action_settings) return true;
            
            return base.OnOptionsItemSelected(item);
        }

        private void fabOnClick(object sender, EventArgs e)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null!).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[]? permissions, Permission[]? grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}