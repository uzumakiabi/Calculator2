using System;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Calculator : Form
    {
        bool isNewEntry = false, isInfinityException = false, isRepeatLastOperation = false; 
        double Result = 0, Operand = 0;
        char chPreviousOperator = new char();
        public Calculator()
        {
            InitializeComponent();
        }
        private void UpdateOperand(object sender, EventArgs e)
        {
            if (!isInfinityException)
            {
                if (isNewEntry)
                {
                    txtResult.Text = "0";
                    isNewEntry = false;
                }
                if (isRepeatLastOperation)
                {
                    chPreviousOperator = '\0';
                    Result = 0;
                }
                if (!(txtResult.Text == "0" && (Button)sender == n0) && !(((Button)sender) == btnDecimalPoint && txtResult.Text.Contains(".")))
                    txtResult.Text = (txtResult.Text == "0" && ((Button)sender) == btnDecimalPoint) ? "0." : ((txtResult.Text == "0") ? ((Button)sender).Text : txtResult.Text + ((Button)sender).Text);
            }
        }
        private void ClearOperator(object sender, EventArgs e)
        {
            isInfinityException = false;
            txtResult.Text = "0";
        }
        private void ChangeSign(object sender, EventArgs e)
        {
            if (!isInfinityException)
                txtResult.Text = (double.Parse(txtResult.Text) * -1).ToString();
        }

        private void OperatorFound(object sender, EventArgs e)
        {
            if (!isInfinityException)
            {
                if (chPreviousOperator == '\0')
                {
                    chPreviousOperator = ((Button)sender).Text[0];
                    Result = double.Parse(txtResult.Text);
                }
                else if (isNewEntry)
                    chPreviousOperator = ((Button)sender).Text[0];
                else
                {
                    Operate(Result, chPreviousOperator, double.Parse(txtResult.Text));
                    chPreviousOperator = ((Button)sender).Text[0];
                }
                isNewEntry = true;
                isRepeatLastOperation = false;
            }
        }
        void Operate(double dblPreviousResult, char chPreviousOperator, double dblOperand)
        {
            switch (chPreviousOperator)
            {
                case '+':
                    txtResult.Text = (Result = (dblPreviousResult + dblOperand)).ToString();
                    break;
                case '-':
                    txtResult.Text = (Result = (dblPreviousResult - dblOperand)).ToString();
                    break;
                case '*':
                    txtResult.Text = (Result = (dblPreviousResult * dblOperand)).ToString();
                    break;
                case '/':
                    if (dblOperand == 0)
                    {
                        txtResult.Text = "Cannot divide by zero";
                        isInfinityException = true;
                    }
                    else
                        txtResult.Text = (Result = (dblPreviousResult / dblOperand)).ToString();
                    break;
            }
        }
        private void Equals(object sender, EventArgs e)
        {
            if (!isInfinityException)
            {
                if (!isRepeatLastOperation)
                {
                    Operand = double.Parse(txtResult.Text);
                    isRepeatLastOperation = true;
                }
                Operate(Result, chPreviousOperator, Operand);
                isNewEntry = true;
            }
        }
        private void ClearAll(object sender, EventArgs e)
        {
            isInfinityException = isRepeatLastOperation = false;
            Operand = Result = 0; txtResult.Text = "0";
            isNewEntry = true;
            chPreviousOperator = '\0';
        }
    }
}
