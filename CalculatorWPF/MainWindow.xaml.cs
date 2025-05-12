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
        bool shouldClearDisplay = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;
            string? number = button?.Content?.ToString();
            
            if (shouldClearDisplay)
            {
                ResultTextBox.Content = number;
                shouldClearDisplay = false;
            }
            else
            {
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
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string newOperatorSymbol = button.Content.ToString();
            string currentResult = (string)ResultTextBox.Content;

            if (hasFirstNumber && operatorSymbol != "" && currentResult != "0" && !shouldClearDisplay)
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
            shouldClearDisplay = true;
        }
        
        private void ClearLastNumber_Click(object sender, RoutedEventArgs e)
        {
            if (!shouldClearDisplay)
            {
                string currentResult = (string)ResultTextBox.Content;
                if (currentResult.Length > 1)
                {
                    ResultTextBox.Content = currentResult.Substring(0, currentResult.Length - 1);
                }
                else
                {
                    ResultTextBox.Content = "0";
                }
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!shouldClearDisplay)
            {
                string currentResult = (string)ResultTextBox.Content;
                secondNumber = double.Parse(currentResult, _cultureInfo);

                double result = PerformOperation(firstNumber, secondNumber, operatorSymbol);
                ResultTextBox.Content = result.ToString(_cultureInfo);
                hasFirstNumber = false;
                operatorSymbol = "";
                shouldClearDisplay = true;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            firstNumber = 0;
            secondNumber = 0;
            operatorSymbol = "";
            hasFirstNumber = false;
            shouldClearDisplay = false;
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
            if (shouldClearDisplay)
            {
                ResultTextBox.Content = "0";
                shouldClearDisplay = false;
            }
            
            if (((string)(ResultTextBox.Content)).Contains("."))
            {
                return;
            }
            ResultTextBox.Content += ".";
        }
    }
}