using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Globalization;


namespace CalculatorApp
{
    [Activity(Label = "CalculatorActivity")]
    public class CalculatorActivity : Activity
    {
        private EditText _firstEditText;
        private EditText _secondEditText;
        private TextView _answerText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.calculator_layout);

            _firstEditText = FindViewById<EditText>(Resource.Id.firstNumberEditText);
            _secondEditText = FindViewById<EditText>(Resource.Id.secondNumberEditText);
            var addButton = FindViewById<Button>(Resource.Id.addButton);
            var subButton = FindViewById<Button>(Resource.Id.subtractButton);
            var divButton = FindViewById<Button>(Resource.Id.divideButton);
            var multiplyButton = FindViewById<Button>(Resource.Id.multiplyButton);
            _answerText = FindViewById<TextView>(Resource.Id.answerTextView);

            if (addButton != null) addButton.Click += AddButton_Click;
            if (subButton != null) subButton.Click += SubButton_Click;
            if (divButton != null) divButton.Click += DivButton_Click;
            if (multiplyButton != null) multiplyButton.Click += MultiplyButton_Click;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {                                    
            var answer = float.Parse(_firstEditText.Text ?? string.Empty) + float.Parse(_secondEditText.Text ?? string.Empty);
            _answerText.Text = answer.ToString(CultureInfo.CurrentCulture);
        }

        private void SubButton_Click(object sender, EventArgs e)
        {
            var answer = float.Parse(_firstEditText.Text ?? string.Empty) - float.Parse(_secondEditText.Text ?? string.Empty);
            _answerText.Text = answer.ToString(CultureInfo.CurrentCulture);
        }

        private void DivButton_Click(object sender, EventArgs e)
        {
            var answer = float.Parse(_firstEditText.Text ?? string.Empty) / float.Parse(_secondEditText.Text ?? string.Empty);
            _answerText.Text = answer.ToString(CultureInfo.CurrentCulture);
        }

        private void MultiplyButton_Click(object sender, EventArgs e)
        {
            var answer = float.Parse(_firstEditText.Text ?? string.Empty) * float.Parse(_secondEditText.Text ?? string.Empty);
            _answerText.Text = answer.ToString(CultureInfo.CurrentCulture);
        }
    }
}