using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmFoundation.Wpf;

namespace Calculator.Models
{
    public class MainWindowModel : ObservableObject
    {
        #region Fields


        #endregion // Fields

        #region Properties


        #endregion // Properties


        public StringBuilder OperationsHistory { get; set; } = new StringBuilder();
        public double DisplayNumber { get; set; }
        public double FirstOperand { get; set; }
        public double SecondOperand { get; set; }
        public int NumFractionalDigits { get; set; }
        public double SavedNumber { get; set; }
        public bool NumberIsSaved { get; set; }
        public bool DisplayNumberHasDecimalPoint { get; set; }

        public int CurrentMode { get; set; }
        public int PreviousMode { get; set; }

        public void ProcessAddition()
        {
            DisplayNumber = FirstOperand + SecondOperand;
            OperationsHistory.Insert(0, String.Format(FirstOperand + " + " +
                SecondOperand + " =\n" + DisplayNumber + "\n\n"));
            FirstOperand = DisplayNumber;
        }

        public void ProcessSubtraction()
        {
            DisplayNumber = FirstOperand - SecondOperand;
            OperationsHistory.Insert(0, String.Format(FirstOperand + " - " +
                SecondOperand + " =\n" + DisplayNumber + "\n\n"));
            FirstOperand = DisplayNumber;
        }

        public void ProcessDivision()
        {
            DisplayNumber = FirstOperand / SecondOperand;
            OperationsHistory.Insert(0, String.Format(FirstOperand +
                " \u00f7 " + SecondOperand + " =\n" + DisplayNumber +
                "\n\n"));
            FirstOperand = DisplayNumber;
        }

        public void ProcessMultiplication()
        {
            DisplayNumber = FirstOperand * SecondOperand;
            OperationsHistory.Insert(0, String.Format(FirstOperand +
                " \u00d7 " + SecondOperand + " =\n" +
                DisplayNumber + "\n\n"));
            FirstOperand = DisplayNumber;
        }
    }
}
