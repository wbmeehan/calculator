using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmFoundation.Wpf;

namespace Calculator.Models
{
    /// <summary>
    /// Calculator model.
    /// </summary>
    public class MainWindowModel : ObservableObject
    {
        #region Properties

        /* Calculator history */
        public StringBuilder OperationsHistory { get; set; } =
            new StringBuilder();
        /* Result displayed */
        public double DisplayNumber { get; set; }
        /* First operand */
        public double FirstOperand { get; set; }
        /* Second operand */
        public double SecondOperand { get; set; }
        /* Number of digits displayed after decimal point */
        public int NumFractionalDigits { get; set; } 
        /* Number saved in memory */
        public double SavedNumber { get; set; }
        /* Is a number saved in memory? */
        public bool NumberIsSaved { get; set; }
        /* Does the result display have a decimal point? */
        public bool DisplayNumberHasDecimalPoint { get; set; }
        /* Current calculator operation mode */
        public int CurrentMode { get; set; }
        /* Previous calculator operation mode */
        public int PreviousMode { get; set; }

        #endregion // Properties

        /// <summary>
        /// Perform addition and store operation in history.
        /// </summary>
        public void Addition()
        {
            DisplayNumber = FirstOperand + SecondOperand;
            OperationsHistory.Insert(0, string.Format(FirstOperand + " + " +
                SecondOperand + " =\n" + DisplayNumber + "\n\n"));
            FirstOperand = DisplayNumber;
        }

        /// <summary>
        /// Perform subtraction and store operation in history.
        /// </summary>
        public void Subtraction()
        {
            DisplayNumber = FirstOperand - SecondOperand;
            OperationsHistory.Insert(0, string.Format(FirstOperand + " - " +
                SecondOperand + " =\n" + DisplayNumber + "\n\n"));
            FirstOperand = DisplayNumber;
        }

        /// <summary>
        /// Perform division and store operation in history.
        /// </summary>
        public void Division()
        {
            DisplayNumber = FirstOperand / SecondOperand;
            OperationsHistory.Insert(0, string.Format(FirstOperand +
            " \u00f7 " + SecondOperand + " =\n" + DisplayNumber +
            "\n\n"));
            FirstOperand = DisplayNumber;
        }

        /// <summary>
        /// Perform multiplication and store operation in history.
        /// </summary>
        public void Multiplication()
        {
            DisplayNumber = FirstOperand * SecondOperand;
            OperationsHistory.Insert(0, string.Format(FirstOperand +
                " \u00d7 " + SecondOperand + " =\n" +
                DisplayNumber + "\n\n"));
            FirstOperand = DisplayNumber;
        }
    }
}
