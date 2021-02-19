using Android.App;
using Android.OS;
using Android.Widget;
using System;


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
            _answerText = FindViewById<TextView>(Resource.Id.answerTextView);

            if (addButton != null) addButton.Click += AddButton_Click;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {                                    
            var answer = int.Parse(_firstEditText.Text ?? string.Empty) + int.Parse(_secondEditText.Text ?? string.Empty);
            _answerText.Text = answer.ToString();
        }
    }
}