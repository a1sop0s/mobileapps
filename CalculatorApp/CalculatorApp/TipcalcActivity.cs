using System;
using System.Globalization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace CalculatorApp
{
    [Activity(Label = "TipcalcActivity")]
    public class TipcalcActivity : Activity
    {
        private EditText _subtotalEditText;
        private EditText _tipEditText;
        private SeekBar _tipSeekBar;
        private TextView _billTotalTextView;
        private TextView _tipTotalTextView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.tipcalc_layout);

            _subtotalEditText = FindViewById<EditText>(Resource.Id.subTotalEditText);
            _tipEditText = FindViewById<EditText>(Resource.Id.tipEditText);
            _tipSeekBar = FindViewById<SeekBar>(Resource.Id.tipSeekBar);
            _billTotalTextView = FindViewById<TextView>(Resource.Id.billTotalTextView);
            _tipTotalTextView = FindViewById<TextView>(Resource.Id.tipTotalTextView);

            _tipSeekBar.ProgressChanged += TipSeekBar_ProgressChanged;
            _tipEditText.TextChanged += _tipEditText_TextChanged;
        }

        private void _tipEditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            _tipSeekBar.Progress = int.Parse(_tipEditText.Text ?? "0");
            CalculateTip();
        }

        private void TipSeekBar_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            _tipEditText.Text = e.Progress.ToString();
        }

        private void CalculateTip()
        {
            var subTotal = double.Parse(_subtotalEditText.Text ?? "0");
            var tipTotal = 0.01 * int.Parse(_tipEditText.Text ?? "0") * subTotal;

            _tipTotalTextView.Text = "Tip total: " + tipTotal;
            _billTotalTextView.Text = "Bill total: " + (subTotal + tipTotal);
        }
    }
}