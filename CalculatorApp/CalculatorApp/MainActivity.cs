using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;

namespace CalculatorApp
{
    [Activity(MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var calcButton = FindViewById<Button>(Resource.Id.toCalcButton);
            var tipCalcButton = FindViewById<Button>(Resource.Id.toTipCalcButton);

            if (calcButton != null) calcButton.Click += CalcButton_Click;
            if (tipCalcButton != null) tipCalcButton.Click += TipCalcButton_Click;
        }

        private void TipCalcButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(TipcalcActivity));
            StartActivity(intent);
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CalculatorActivity));
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, 
            [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}