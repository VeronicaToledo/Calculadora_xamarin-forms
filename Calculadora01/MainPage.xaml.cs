using System;
using Xamarin.Forms;

namespace Calculadora01
{
    public partial class MainPage : ContentPage
    {
        private string currentExpression = "";
        private bool isResult = false;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSelectNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string selectedNumber = button.Text;

            if (isResult)
            {
                resultText.Text = "0";
                isResult = false;
            }

            if (resultText.Text == "0")
            {
                resultText.Text = selectedNumber;
            }
            else
            {
                resultText.Text += selectedNumber;
            }

            currentExpression += selectedNumber;
            operationText.Text = currentExpression;
        }

        private void OnSelectOperator(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string selectedOperator = button.Text;

            if (!string.IsNullOrEmpty(currentExpression) && !IsOperator(currentExpression[currentExpression.Length - 1]))
            {
                resultText.Text += selectedOperator;
                currentExpression += selectedOperator;
                operationText.Text = currentExpression;
            }
        }

        private void OnSelectDecimal(object sender, EventArgs e)
        {
            if (!resultText.Text.Contains("."))
            {
                resultText.Text += ".";
                currentExpression += ".";
                operationText.Text = currentExpression;
            }
        }

        private void OnSelectParenthesis(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string parenthesis = button.Text;

            if (isResult)
            {
                resultText.Text = "";
                isResult = false;
            }

            if (parenthesis == "(" && resultText.Text == "0")
            {
                resultText.Text = parenthesis;
            }
            else
            {
                resultText.Text += parenthesis;
            }
            currentExpression += parenthesis;
            operationText.Text = currentExpression;
        }

        private void OnClearEntry(object sender, EventArgs e)
        {
            resultText.Text = "0";
            currentExpression = "";
            operationText.Text = currentExpression;
        }

        private void OnCalculate(object sender, EventArgs e)
        {
            try
            {
                // Evaluar la expresión usando DataTable
                var result = new System.Data.DataTable().Compute(currentExpression.Replace("X", "*"), null);
                string operationWithResult = $"{currentExpression}={result}";
                resultText.Text = result.ToString();
                currentExpression = result.ToString();
                isResult = true;
                operationText.Text = operationWithResult;
            }
            catch (Exception)
            {
                resultText.Text = "Error";
                currentExpression = "";
                operationText.Text = currentExpression;
            }
        }

        private void OnDeleteLastCharacter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentExpression))
            {
                currentExpression = currentExpression.Remove(currentExpression.Length - 1);
                resultText.Text = currentExpression == "" ? "0" : currentExpression;
                operationText.Text = currentExpression;
            }
        }

        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }
    }
}
