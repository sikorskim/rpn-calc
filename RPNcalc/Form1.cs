using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPNcalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            startup();
        }

        RPNcalculator rpn = new RPNcalculator();
        string input = "";
        List<string> list = new List<string>();
        int mode = 0;

        void startup()
        {
            //enable keyDown event to form
            this.KeyPreview = true;
            foreach (Control control in this.Controls)
            {
                control.TabStop = false;
            }
            this.ActiveControl = null;
        }

        void setMode()
        {
            mode++;
            if (mode % 2 == 0)
            {
                label6.Text = "MAT";
                button10.Enabled = true;
                button17.Enabled = true;
                button18.Enabled = true;
                button19.Enabled = true;
                button22.Enabled = true;
                button12.Text = ",";
            }
            else
            {
                button10.Enabled = false;
                button17.Enabled = false;
                button18.Enabled = false;
                button19.Enabled = false;
                button22.Enabled = false;
                label6.Text = "DT";
                button12.Text = ":";
            }
        }

        void addToInput(string s)
        {
            input += s;
            textBox1.Text = input;
        }

        void addToDisplay(string s)
        {
            input = s;
            list.Add(input);
            int displayRows = 5;
            string[] displayedList = new string[displayRows];

            int listCnt = list.Count;
            if (listCnt > 5)
            {
                displayedList = list.GetRange(listCnt - 5, 5).ToArray();
            }
            else
            {
                for (int i = 0; i < listCnt; i++)
                {
                    displayedList[i] = list[i];
                }
            }
            listBox1.DataSource = displayedList;
            listBox1.Refresh();
        }

        void setResult(string result, bool changeDisplay)
        {
            if (changeDisplay)
            {
                //try
                //{
                int count = list.Count;
                if (count < 2)
                {
                    count = 2;
                }
                    list = list.GetRange(0, count - 2);
                    listBox1.DataSource = list;
                    listBox1.Refresh();
                //}
                //catch (Exception)
                //{
                //    listBox1.DataSource = null;
                //    listBox1.Refresh();
                //}
            }
            input = result;
            textBox1.Text = input;
        }

        void clearInput()
        {
            input = "";
            textBox1.Clear();
        }

        void drop()
        {
            rpn.remFromStack();
            clearInput();
            list = list.Take(list.Count - 1).ToList();
            listBox1.DataSource = list;
            listBox1.Refresh();
        }

        void clearStack()
        {
            rpn.clearStack();
            textBox1.Clear();
            list.Clear();
            listBox1.DataSource = null;
            listBox1.Refresh();
        }

        bool enter(bool changeDisplay)
        {
            bool inputCorrect;

            if (mode % 2 == 0)
            {
                inputCorrect = rpn.checkDouble(input);
            }
            else
            {
                inputCorrect = rpn.checkDateTime(input);
            }

            if (inputCorrect && changeDisplay)
            {
                rpn.enter(input);
                addToDisplay(input);
                clearInput();
                return true;
            }
            else if (inputCorrect && !changeDisplay)
            {
                rpn.enter(input);
                return true;
            }
            else
            {
                textBox1.Text = "ERR:check input";
                return false;
            }
        }

        void add()
        {
            if (enter(true) && rpn.countSTack() > 1)
            {
                if (mode % 2 == 0)
                {
                    setResult(rpn.add(),true);
                }
                else
                {
                    setResult(rpn.addDateTime(),true);
                }
            }
        }

        void substract()
        {
            if (enter(true) && rpn.countSTack() > 1)
            {               
                if (mode % 2 == 0)
                {
                    setResult(rpn.substract(), true);
                }
                else
                {
                    setResult(rpn.substractDateTime(), true);
                }
            }
        }

        void multiply()
        {
            if (enter(true) && rpn.countSTack() > 1)
            {
                setResult(rpn.multiply(),true);
            }
        }

        void divide()
        {
            if (enter(true) && rpn.countSTack() > 1)
            {
                setResult(rpn.divide(),true);
            }
        }

        void inv()
        {
            if (enter(false))
            {
                setResult(rpn.inv(),false);
            }
        }

        void sqrt()
        {
            if (enter(false))
            {
                setResult(rpn.sqrt(),false);
            }
        }

        void pow()
        {
            if (enter(true) && rpn.countSTack() > 1)
            {
                setResult(rpn.pow(),true);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (enter(false))
            {
                inv();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            addToInput("0");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addToInput("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addToInput("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addToInput("3");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addToInput("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addToInput("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            addToInput("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            addToInput("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            addToInput("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            addToInput("9");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            enter(true);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            add();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            substract();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            multiply();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (mode % 2 == 0)
            {
                addToInput(",");
            }
            else
            {
                addToInput(":");
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            divide();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            drop();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            sqrt();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            pow();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            clearInput();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                enter(true);
            }
            else if (e.KeyData == Keys.NumPad0)
            {
                addToInput("0");
            }
            else if (e.KeyData == Keys.NumPad1)
            {
                addToInput("1");
            }
            else if (e.KeyData == Keys.NumPad2)
            {
                addToInput("2");
            }
            else if (e.KeyData == Keys.NumPad3)
            {
                addToInput("3");
            }
            else if (e.KeyData == Keys.NumPad4)
            {
                addToInput("4");
            }
            else if (e.KeyData == Keys.NumPad5)
            {
                addToInput("5");
            }
            else if (e.KeyData == Keys.NumPad6)
            {
                addToInput("6");
            }
            else if (e.KeyData == Keys.NumPad7)
            {
                addToInput("7");
            }
            else if (e.KeyData == Keys.NumPad8)
            {
                addToInput("8");
            }
            else if (e.KeyData == Keys.NumPad9)
            {
                addToInput("9");
            }
            else if (e.KeyData == Keys.Subtract)
            {
                substract();
            }
            else if (e.KeyData == Keys.Multiply)
            {
                multiply();
            }
            else if (e.KeyData == Keys.Add)
            {
                add();
            }
            else if (e.KeyData == Keys.Divide)
            {
                divide();
            }
            else if (e.KeyData == Keys.Delete)
            {
                clearStack();
            }
            else if (e.KeyData == Keys.Back)
            {
                drop();
            }
            else if (e.KeyData == Keys.Oemcomma)
            {
                if (mode % 2 == 0)
                {
                    addToInput(",");
                }
                else
                {
                    addToInput(".");
                }
            }
            else if (e.KeyData == Keys.OemSemicolon)
            {
                addToInput(":");
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            setMode();
        }

        private void button23_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            input = textBox1.Text;
        }
    }
}
