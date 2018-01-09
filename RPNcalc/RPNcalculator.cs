using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNcalc
{
    public class RPNcalculator
    {
        Stack<Number> stack = null;

        public RPNcalculator()
        {
            stack = new Stack<Number>();
        }

        public void enter(string str)
        {
            pushToStack(str);
        }

        public bool checkDouble(string str)
        {
            return Double.TryParse(str, out double d);
        }

        public bool checkDateTime(string str)
        {
            return DateTime.TryParse(str, out DateTime dt);
        }

        Number[] popNumbers()
        {
            Number[] numbers = new Number[2];
            Number a = popFromStack();
            Number b = popFromStack();
            numbers[0] = a;
            numbers[1] = b;
            return numbers;
        }

        public string add()
        {
            Number[] num = popNumbers();
            Number c = num[0] + num[1];
            return c.getValue();
        }

        public string addDateTime()
        {
            Number[] num = popNumbers();
            DateTime dt1 = DateTime.Parse(num[0].getValue());
            DateTime dt2 = DateTime.Parse(num[1].getValue());
            TimeSpan ts = dt1 - dt2;
            dt1=dt1.Add(ts);
            return dt1.ToString();
        }
        public string substractDateTime()
        {
            Number[] num = popNumbers();
            DateTime dt1 = DateTime.Parse(num[0].getValue());
            DateTime dt2 = DateTime.Parse(num[1].getValue());
            TimeSpan ts = dt1 - dt2;
            return ts.ToString();
        }

        public string substract()
        {
            Number[] num = popNumbers();
            Number c = num[0] - num[1];
            return c.getValue();
        }

        public string multiply()
        {
            Number[] num = popNumbers();
            Number c = num[0] * num[1];
            return c.getValue();
        }

        public string divide()
        {
            Number[] num = popNumbers();
            Number c = num[0] / num[1];
            return c.getValue();
        }

        public string pow()
        {
            Number[] num = popNumbers();
            Number c = num[0] ^ num[1];
            return c.getValue();
        }

        public string sqrt()
        {
            Number a = popFromStack();
            return a.sqrt().getValue();
        }

        public string inv()
        {
            Number a = popFromStack();
            return a.inv().getValue();
        }

        #region stack operations

        void pushToStack(string s)
        {
            Number num = new Number(s);
            stack.Push(num);
        }

        Number popFromStack()
        {
            try
            {
                return stack.Pop();
            }
            catch (Exception e)
            {
                return new Number("ERR: stack is empty");
            }
        }

        public int countSTack()
        {
            return stack.Count;
        }

        public void clearStack()
        {
            stack.Clear();
        }

        public void remFromStack()
        {
            try
            {
                stack.Pop();
            }
            catch (Exception)
            { }
        }
        #endregion
    }
}