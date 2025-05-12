using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace CalculatorWPF
{
    public partial class MainWindow : Window
    {
        private State _state = State.Number;
        CultureInfo _cultureInfo = CultureInfo.GetCultureInfo("en-En");
        double firstNumber = 0;
        double secondNumber = 0;
        string operatorSymbol = "";
        bool hasFirstNumber = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            string? number = button?.Content?.ToString();
            string currentResult = (string)ResultTextBox.Content;

            if (currentResult == "0")
            {
                ResultTextBox.Content = number;
            }
            else
            {
                ResultTextBox.Content += number;
            }
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string newOperatorSymbol = button.Content.ToString();
            string currentResult = (string)ResultTextBox.Content;

            if (hasFirstNumber && operatorSymbol != "" && currentResult != "0")
            {
                secondNumber = double.Parse(currentResult, _cultureInfo);
                double result = PerformOperation(firstNumber, secondNumber, operatorSymbol);
                ResultTextBox.Content = result.ToString(_cultureInfo);
                firstNumber = result;
            }
            else if (currentResult != "0")
            {
                firstNumber = double.Parse(currentResult, _cultureInfo);
                hasFirstNumber = true;
            }

            operatorSymbol = newOperatorSymbol;
            ResultTextBox.Content = "0";
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            string currentResult = (string)ResultTextBox.Content;
            secondNumber = double.Parse(currentResult, _cultureInfo);

            double result = PerformOperation(firstNumber, secondNumber, operatorSymbol);
            ResultTextBox.Content = result.ToString(_cultureInfo);
            hasFirstNumber = false;
            operatorSymbol = "";
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            firstNumber = 0;
            secondNumber = 0;
            operatorSymbol = "";
            hasFirstNumber = false;
            ResultTextBox.Content = "0";
        }

        private double PerformOperation(double firstNumber, double secondNumber, string operatorSymbol)
        {
            double result = 0;

            switch (operatorSymbol)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "*":
                    result = firstNumber * secondNumber;
                    break;
                case "/":
                    result = firstNumber / secondNumber;
                    break;
            }

            return result;
        }

        private void DotButton_Click(object sender, RoutedEventArgs e)
        {
            if (((string)(ResultTextBox.Content)).Contains("."))
            {
                return;
            }
            ResultTextBox.Content += ".";
        }
    }
}