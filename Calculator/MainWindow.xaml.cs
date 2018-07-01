using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Operation modes
        private const int DEFAULT = 0;
        private const int ADD = 1;
        private const int SUBTRACT = 2;
        private const int MULTIPLY = 3;
        private const int DIVIDE = 4;
        private const int EQUALS = 5;

        private const int NOT_FRACTIONAL_INPUT = -1;

        double savedNumber = 0;
        bool numberSaved = false;
        bool viewingHistory = false;
        int numFractionalDigits = NOT_FRACTIONAL_INPUT;
        double displayNumber = 0;
        double firstOperand = 0;
        double secondOperand = 0;
        int previousMode = DEFAULT;
        int currentMode = DEFAULT;

        StringBuilder operationsHistory = new StringBuilder();



        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnNumClick(object sender, RoutedEventArgs e)
        {
            Button pushedButton = (Button)sender;
            int buttonValue = Convert.ToInt16(pushedButton.Content.ToString());

            if (currentMode == EQUALS)
            {
                enterMode(DEFAULT);
            }

            if (numFractionalDigits == NOT_FRACTIONAL_INPUT)
            {
                secondOperand *= 10;
                secondOperand += buttonValue;
                displayNumber = secondOperand;
            }
            else
            {
                numFractionalDigits++;
                secondOperand += buttonValue / (Math.Pow(10, numFractionalDigits));
                displayNumber = secondOperand;
            }


            refreshDisplay();
        }

        private void btnBackspaceClick(object sender, RoutedEventArgs e)
        {


            if (numFractionalDigits == NOT_FRACTIONAL_INPUT)
            {
                secondOperand -= secondOperand % 10;
                secondOperand /= 10;
                displayNumber = secondOperand;
            }
            else
            {
                numFractionalDigits--;
                double adjustment = Math.Pow(10, Math.Max(0, numFractionalDigits));
                secondOperand = Math.Floor(secondOperand * adjustment) / adjustment;
                displayNumber = secondOperand;
            }

            refreshDisplay();
        }

        private void btnCEClick(object sender, RoutedEventArgs e)
        {
            displayNumber = 0;
            secondOperand = 0;
            numFractionalDigits = NOT_FRACTIONAL_INPUT;

            refreshDisplay();
        }

        private void btnCClick(object sender, RoutedEventArgs e)
        {
            displayNumber = 0;
            firstOperand = 0;
            secondOperand = 0;
            numFractionalDigits = NOT_FRACTIONAL_INPUT;

            refreshDisplay();
        }


        private void btnPlusClick(object sender, RoutedEventArgs e)
        {
           enterMode(ADD);
           processOperation();
        }

        private void btnMinusClick(object sender, RoutedEventArgs e)
        {
            enterMode(SUBTRACT);
            processOperation();
        }

        private void btnMultiplyClick(object sender, RoutedEventArgs e)
        {
            enterMode(MULTIPLY);
            processOperation();
        }

        private void btnDivideClick(object sender, RoutedEventArgs e)
        {
            enterMode(DIVIDE);
            processOperation();
        }

        private void btnEqualsClick(object sender, RoutedEventArgs e)
        {
            enterMode(EQUALS);
            processOperation();
        }

        private void btnPlusMinusClick(object sender, RoutedEventArgs e)
        {
            if (currentMode == EQUALS)
            {
                firstOperand *= -1;
                displayNumber = firstOperand;
            }
            else
            {
                secondOperand *= -1;
                displayNumber = secondOperand;
            }

            refreshDisplay();
        }

        private void btnDecimalPointClick(object sender, RoutedEventArgs e)
        {

            if (currentMode == EQUALS)
            {
                enterMode(DEFAULT);
                displayNumber = 0;
            }

            if (numFractionalDigits == NOT_FRACTIONAL_INPUT)
            {
                numFractionalDigits = 0;
            }

            refreshDisplay();
        }

        private void btnMemSaveClick(object sender, RoutedEventArgs e)
        {
            savedNumber = displayNumber;
            numberSaved = true;
        }

        private void btnHistoryClick(object sender, RoutedEventArgs e)
        {
            if (!viewingHistory)
            {
                historyScrollDisplay.Visibility = System.Windows.Visibility.Visible;
                txtHistory.Text = operationsHistory.ToString().TrimEnd('\n');
                viewingHistory = true;
            } else
            {
                historyScrollDisplay.Visibility = System.Windows.Visibility.Collapsed;
                viewingHistory = false;
            }
        }

        private void btnMemRecallClick(object sender, RoutedEventArgs e)
        {
            if (numberSaved == false)
            {
                return;
            }

            if (currentMode == EQUALS)
            {
                firstOperand = savedNumber;
                displayNumber = firstOperand;
            }
            else
            {
                secondOperand = savedNumber;
                displayNumber = secondOperand;
            }

            refreshDisplay();
        }

        private void btnMemClearClick(object sender, RoutedEventArgs e)
        {
            numberSaved = false;
        }

        private void processOperation()
        {
            switch (previousMode)
            {
                case ADD:
                    processAddition();
                    break;
                case SUBTRACT:
                    processSubtraction();
                    break;
                case DIVIDE:
                    processDivision();
                    break;
                case MULTIPLY:
                    processMultiplication();
                    break;
                case EQUALS:
                    break;
                default:
                    firstOperand = secondOperand;
                    break;
            }

            secondOperand = 0;
            numFractionalDigits = NOT_FRACTIONAL_INPUT;
            refreshDisplay();
        }

        private void processAddition()
        {
            displayNumber = firstOperand + secondOperand;
            operationsHistory.Insert(0, String.Format(firstOperand + " + " + secondOperand + " =\n" + displayNumber + "\n\n"));
            firstOperand = displayNumber;
        }

        private void processSubtraction()
        {
            displayNumber = firstOperand - secondOperand;
            operationsHistory.Insert(0, String.Format(firstOperand + " - " + secondOperand + " =\n" + displayNumber + "\n\n"));
            firstOperand = displayNumber;
        }

        private void processDivision()
        {
            displayNumber = firstOperand / secondOperand;
            operationsHistory.Insert(0, String.Format(firstOperand + " \u00f7 " + secondOperand + " =\n" + displayNumber + "\n\n"));
            firstOperand = displayNumber;
        }

        private void processMultiplication()
        {
            displayNumber = firstOperand * secondOperand;
            operationsHistory.Insert(0, String.Format(firstOperand + " \u00d7 " + secondOperand + " =\n" + displayNumber + "\n\n"));
            firstOperand = displayNumber;
        }

        private void enterMode(int newMode)
        {
            previousMode = currentMode;
            currentMode = newMode;
        }

        private void refreshDisplay()
        {
            if (numFractionalDigits == NOT_FRACTIONAL_INPUT)
            {
                txtDisplay.Text = String.Format("{0}", displayNumber);
            } else
            {
                string fmt = "{0:.";
                for (int i = 0; i < numFractionalDigits; i++)
                {
                    fmt += "0";
                }
                fmt += "}";
                txtDisplay.Text = String.Format(fmt, displayNumber);
                if (numFractionalDigits == 0)
                {
                    txtDisplay.Text += ".";
                }
            }
        }


    }
}
