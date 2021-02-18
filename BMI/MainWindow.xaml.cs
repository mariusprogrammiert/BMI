using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BMI
{
    public partial class MainWindow : Window
    {
        private readonly BodyMassIndexLogic logic;
        private readonly Regex regexOnlyNumbers;

        public MainWindow()
        {
            InitializeComponent();

            logic = new BodyMassIndexLogic();
            logic.ShowFormatError += ShowFormatError;
            logic.ShowResult += ShowResult;

            regexOnlyNumbers = new Regex("[^0-9]+");
        }

        private void TextBoxInputValidation(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length >= 3)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = regexOnlyNumbers.IsMatch(e.Text);
            }
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            PerformCalculation();
        }

        private void PerformCalculation()
        {
            try
            {
                logic.EvaluateData(Convert.ToInt32(textboxSize.Text), Convert.ToInt32(textboxWeight.Text));
            }
            catch (FormatException)
            {
                ShowFormatError();
            }
        }

        private void ShowFormatError()
        {
            MessageBox.Show("Keine gültigen Eingaben!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ShowResult(string result)
        {
            labelResult.Content = result;
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PerformCalculation();
            }
        }
    }
}
