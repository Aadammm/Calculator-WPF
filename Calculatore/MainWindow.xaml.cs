using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Calculatore.MainWindow;

namespace Calculatore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            if (historyLabel.Content.ToString().Contains("="))
            {
                return;
            }
            if (double.TryParse(resultLabel.Content.ToString(), out double newNumber))
            {
                historyLabel.Content += newNumber.ToString() + "=";
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimplyMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Substraction:
                        result = SimplyMath.Substraction(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimplyMath.Divide(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimplyMath.Multiply(lastNumber, newNumber);
                        break;
                }
            }
            resultLabel.Content = result.ToString();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());
            resultLabel.Content = resultLabel.Content.ToString() == "0" ?
               $"{selectedValue}" :
               $"{resultLabel.Content}{selectedValue}";
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = 0;
            historyLabel.Content = string.Empty;
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out double tempNumber))
            {
                tempNumber = tempNumber / 100;
                if (lastNumber != 0)
                {
                    tempNumber *= lastNumber;
                }
                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }

            if (sender == multiplicationButton)
            {
                historyLabel.Content = lastNumber + (sender as Button).Content.ToString();
                selectedOperator = SelectedOperator.Multiplication;
            }
            if (sender == divideButton)
            {
                historyLabel.Content = lastNumber + (sender as Button).Content.ToString();

                selectedOperator = SelectedOperator.Division;
            }
            if (sender == plusButton)
            {
                historyLabel.Content = lastNumber + (sender as Button).Content.ToString();

                selectedOperator = SelectedOperator.Addition;
            }
            if (sender == minusButton)
            {
                historyLabel.Content = lastNumber + (sender as Button).Content.ToString();

                selectedOperator = SelectedOperator.Substraction;
            }
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        public enum SelectedOperator
        {
            Addition,
            Substraction,
            Division,
            Multiplication
        }
        private void PointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains("."))
            {
                resultLabel.Content += ".";
            }
        }

        public class SimplyMath
        {
            public static double Add(double n1, double n2) => n1 + n2;
            public static double Substraction(double n1, double n2) => n1 - n2;
            public static double Multiply(double n1, double n2) => n1 * n2;
            public static double Divide(double n1, double n2)
            {
                if (n2 == 0)
                {
                    MessageBox.Show("Division by 0 is not supported", "Wrong Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                    return double.NaN;
                }
                return n1 / n2;
            }
        }



    }
}