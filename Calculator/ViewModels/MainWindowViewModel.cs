using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Calculator.Models;
using MvvmFoundation.Wpf;

namespace Calculator.ViewModels
{
    /// <summary>
    /// Calculator view model.
    /// </summary>
    public class MainWindowViewModel : ObservableObject
    {
        #region Fields

        /* Operation modes */
        private const int Default = 0;
        private const int Add = 1;
        private const int Subtract = 2;
        private const int Multiply = 3;
        private const int Divide = 4;
        private const int Equal = 5;

        /* Calculation model */
        private MainWindowModel _calculatorModel = new MainWindowModel();

        /* Click commands */
        private ICommand _numClickCommand;
        private ICommand _backspaceClickCommand;
        private ICommand _clearEntryClickCommand;
        private ICommand _clearAllClickCommand;
        private ICommand _plusClickCommand;
        private ICommand _minusClickCommand;
        private ICommand _multiplyClickCommand;
        private ICommand _divideClickCommand;
        private ICommand _equalsClickCommand;
        private ICommand _plusMinusClickCommand;
        private ICommand _decimalPointClickCommand;
        private ICommand _memSaveClickCommand;
        private ICommand _memClearClickCommand;
        private ICommand _memRecallClickCommand;
        private ICommand _historyClickCommand;

        #endregion

        #region Public Properties/Commands

        /* Is the calculation history being viewed? */
        public bool ViewingHistory { get; set; }
        /* Result text */
        public string DisplayText { get; set; }
        /* History text */
        public string HistoryText { get; set; }

        #region Calculator model variables
        public double SavedNumber
        {
            get
            {
                return _calculatorModel.SavedNumber;
            }

            set
            {
                _calculatorModel.SavedNumber = value;
            }
        }

        public bool NumberIsSaved
        {
            get
            {
                return _calculatorModel.NumberIsSaved;
            }

            set
            {
                _calculatorModel.NumberIsSaved = value;
            }
        }

        public bool DisplayNumberHasDecimalPoint
        {
            get
            {
                return _calculatorModel.DisplayNumberHasDecimalPoint;
            }

            set
            {
                _calculatorModel.DisplayNumberHasDecimalPoint = value;
                NumFractionalDigits = 0;
            }
        }

        public int CurrentMode
        {
            get
            {
                return _calculatorModel.CurrentMode;
            }

            set
            {
                _calculatorModel.CurrentMode = value;
            }
        }

        public int PreviousMode
        {
            get
            {
                return _calculatorModel.PreviousMode;
            }

            set
            {
                _calculatorModel.PreviousMode = value;
            }
        }

        public double FirstOperand
        {
            get
            {
                return _calculatorModel.FirstOperand;
            }

            set
            {
                _calculatorModel.FirstOperand = value;
            }
        }

        public double SecondOperand
        {
            get
            {
                return _calculatorModel.SecondOperand;
            }

            set
            {
                _calculatorModel.SecondOperand = value;
            }
        }

        public double DisplayNumber
        {
            get
            {
                return _calculatorModel.DisplayNumber;
            }

            set
            {
                _calculatorModel.DisplayNumber = value;
            }
        }

        public int NumFractionalDigits
        {
            get
            {
                return _calculatorModel.NumFractionalDigits;
            }

            set
            {
                if (value >= 0)
                {
                    _calculatorModel.NumFractionalDigits = value;
                }
                else
                {
                    DisplayNumberHasDecimalPoint = false;
                }
            }
        }

        public StringBuilder OperationsHistory
        {
            get
            {
                return _calculatorModel.OperationsHistory;
            }
        }
        #endregion

        #region Button click commands
        public ICommand NumClickCommand
        {
            get
            {
                if (_numClickCommand == null)
                {
                    _numClickCommand = new RelayCommand<string>(NumClick);

                }
                return _numClickCommand;
            }

        }

        public ICommand BackspaceClickCommand
        {
            get
            {
                if (_backspaceClickCommand == null)
                {
                    _backspaceClickCommand = new RelayCommand(BackspaceClick);

                }
                return _backspaceClickCommand;
            }
        }

        public ICommand CEClickCommand
        {
            get
            {
                if (_clearEntryClickCommand == null)
                {
                    _clearEntryClickCommand = new RelayCommand(CEClick);

                }
                return _clearEntryClickCommand;
            }
        }

        public ICommand ClearAllClickCommand
        {
            get
            {
                if (_clearAllClickCommand == null)
                {
                    _clearAllClickCommand = new RelayCommand(CClick);

                }
                return _clearAllClickCommand;
            }
        }

        public ICommand PlusClickCommand
        {
            get
            {
                if (_plusClickCommand == null)
                {
                    _plusClickCommand = new RelayCommand(PlusClick);

                }
                return _plusClickCommand;
            }
        }

        public ICommand MinusClickCommand
        {
            get
            {
                if (_minusClickCommand == null)
                {
                    _minusClickCommand = new RelayCommand(MinusClick);
                }
                return _minusClickCommand;
            }
        }

        public ICommand MultiplyClickCommand
        {
            get
            {
                if (_multiplyClickCommand == null)
                {
                    _multiplyClickCommand = new RelayCommand(MultiplyClick);
                }

                return _multiplyClickCommand;
            }
        }

        public ICommand DivideClickCommand
        {
            get
            {
                if (_divideClickCommand == null)
                {
                    _divideClickCommand = new RelayCommand(DivideClick);
                }

                return _divideClickCommand;
            }
        }

        public ICommand EqualsClickCommand
        {
            get
            {
                if (_equalsClickCommand == null)
                {
                    _equalsClickCommand = new RelayCommand(EqualsClick);
                }

                return _equalsClickCommand;
            }
        }

        public ICommand PlusMinusClickCommand
        {
            get
            {
                if (_plusMinusClickCommand == null)
                {
                    _plusMinusClickCommand = new RelayCommand(PlusMinusClick);
                }

                return _plusMinusClickCommand;
            }
        }

        public ICommand DecimalPointClickCommand
        {
            get
            {
                if (_decimalPointClickCommand == null)
                {
                    _decimalPointClickCommand = new RelayCommand(DecimalPointClick);
                }

                return _decimalPointClickCommand;
            }
        }

        public ICommand MemSaveClickCommand
        {
            get
            {
                if (_memSaveClickCommand == null)
                {
                    _memSaveClickCommand = new RelayCommand(MemSaveClick);
                }

                return _memSaveClickCommand;
            }
        }

        public ICommand MemClearClickCommand
        {
            get
            {
                if (_memClearClickCommand == null)
                {
                    _memClearClickCommand = new RelayCommand(MemClearClick);
                }

                return _memClearClickCommand;
            }
        }

        public ICommand MemRecallClickCommand
        {
            get
            {
                if (_memRecallClickCommand == null)
                {
                    _memRecallClickCommand = new RelayCommand(MemRecallClick);
                }

                return _memRecallClickCommand;
            }
        }

        public ICommand HistoryClickCommand
        {
            get
            {
                if (_historyClickCommand == null)
                {
                    _historyClickCommand = new RelayCommand(HistoryClick);
                }

                return _historyClickCommand;
            }
        }
        #endregion

        #endregion // Properties

        #region Private Helpers

        /// <summary>
        /// Append clicked number to end of displayed number.
        /// </summary>
        /// <param name="buttonValue">The value of the number clicked.</param>
        private void NumClick(string buttonValue)
        {
            if (CurrentMode == Equal)
            {
                EnterMode(Default);
            }

            if (!DisplayNumberHasDecimalPoint)
            {
                SecondOperand *= 10;
                SecondOperand += int.Parse(buttonValue);
                DisplayNumber = SecondOperand;
            }
            else
            {
                NumFractionalDigits++;
                SecondOperand += int.Parse(buttonValue) / Math.Pow(10, NumFractionalDigits);
                DisplayNumber = SecondOperand;
            }

            RefreshDisplay();
        }

        /// <summary>
        /// Perform backspace.
        /// </summary>
        private void BackspaceClick()
        {
            if (!DisplayNumberHasDecimalPoint)
            {
                SecondOperand -= SecondOperand % 10;
                SecondOperand /= 10;
                DisplayNumber = SecondOperand;
            }
            else
            {
                NumFractionalDigits--;
                double adjustment = Math.Pow(10, Math.Max(0, NumFractionalDigits));
                SecondOperand = Math.Floor(SecondOperand * adjustment) / adjustment;
                DisplayNumber = SecondOperand;
            }

            RefreshDisplay();
        }

        /// <summary>
        /// Clear entry.
        /// </summary>
        private void CEClick()
        {
            DisplayNumber = 0;
            SecondOperand = 0;
            DisplayNumberHasDecimalPoint = false;

            RefreshDisplay();
        }

        /// <summary>
        /// Clear all.
        /// </summary>
        private void CClick()
        {
            DisplayNumber = 0;
            FirstOperand = 0;
            SecondOperand = 0;
            DisplayNumberHasDecimalPoint = false;

            RefreshDisplay();
        }

        /// <summary>
        /// Queue addition.
        /// </summary>
        private void PlusClick()
        {
            EnterMode(Add);
            ProcessOperation();
        }

        /// <summary>
        /// Queue subtraction.
        /// </summary>
        private void MinusClick()
        {
            EnterMode(Subtract);
            ProcessOperation();
        }

        /// <summary>
        /// Queue multiplication.
        /// </summary>
        private void MultiplyClick()
        {
            EnterMode(Multiply);
            ProcessOperation();
        }

        /// <summary>
        /// Queue division.
        /// </summary>
        private void DivideClick()
        {
            EnterMode(Divide);
            ProcessOperation();
        }

        /// <summary>
        /// Perform equals.
        /// </summary>
        private void EqualsClick()
        {
            EnterMode(Equal);
            ProcessOperation();
        }

        /// <summary>
        /// Change sign of displayed result.
        /// </summary>
        private void PlusMinusClick()
        {
            if (CurrentMode == Equal)
            {
                FirstOperand *= -1;
                DisplayNumber = FirstOperand;
            }
            else
            {
                SecondOperand *= -1;
                DisplayNumber = SecondOperand;
            }

            RefreshDisplay();
        }

        /// <summary>
        /// Append decimal point to displayed number.
        /// </summary>
        private void DecimalPointClick()
        {
            if (CurrentMode == Equal)
            {
                EnterMode(Default);
                DisplayNumber = 0;
            }

            if (!DisplayNumberHasDecimalPoint)
            {
                NumFractionalDigits = 0;
                DisplayNumberHasDecimalPoint = true;
            }

            RefreshDisplay();
        }

        /// <summary>
        /// Save displayed number into memory.
        /// </summary>
        private void MemSaveClick()
        {
            SavedNumber = DisplayNumber;
            NumberIsSaved = true;
        }

        /// <summary>
        /// Recall result stored in memory.
        /// </summary>
        private void MemRecallClick()
        {
            if (NumberIsSaved == false)
            {
                return;
            }

            if (CurrentMode == Equal)
            {
                FirstOperand = SavedNumber;
                DisplayNumber = FirstOperand;
            }
            else
            {
                SecondOperand = SavedNumber;
                DisplayNumber = SecondOperand;
            }

            RefreshDisplay();
        }

        /// <summary>
        /// Clear result saved in memory.
        /// </summary>
        private void MemClearClick()
        {
            NumberIsSaved = false;
        }

        /// <summary>
        /// Display calculator history.
        /// </summary>
        private void HistoryClick()
        {
            if (!ViewingHistory)
            {
                HistoryText = OperationsHistory.ToString().TrimEnd('\n');
                ViewingHistory = true;
            }
            else
            {
                ViewingHistory = false;
            }
        }
        
        /// <summary>
        /// Process +,-,*,/,= operations.
        /// </summary>
        private void ProcessOperation()
        {
            switch (PreviousMode)
            {
                case Add:
                    _calculatorModel.Addition();
                    break;
                case Subtract:
                    _calculatorModel.Subtraction();
                    break;
                case Divide:
                    _calculatorModel.Division();
                    break;
                case Multiply:
                    _calculatorModel.Multiplication();
                    break;
                case Equal:
                    break;
                default:
                    FirstOperand = SecondOperand;
                    break;
            }

            SecondOperand = 0;
            DisplayNumberHasDecimalPoint = false;
            RefreshDisplay();
        }
   
        /// <summary>
        /// Enter new mode.
        /// </summary>
        /// <param name="newMode">New mode.</param>
        private void EnterMode(int newMode)
        {
            PreviousMode = CurrentMode;
            CurrentMode = newMode;
        }

        /// <summary>
        /// Refresh displayed calculator result.
        /// </summary>
        private void RefreshDisplay()
        {
            if (!DisplayNumberHasDecimalPoint)
            {
                DisplayText = string.Format("{0}", DisplayNumber);
            }
            else
            {
                string fmt = "{0:.";
                for (int i = 0; i < NumFractionalDigits; i++)
                {
                    fmt += "0";
                }

                fmt += "}";
                DisplayText = string.Format(fmt, DisplayNumber);
                if (NumFractionalDigits == 0)
                {
                    DisplayText += ".";
                }
            }
        }

        #endregion
    }
}
