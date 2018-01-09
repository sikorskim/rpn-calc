using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNcalc
{
    public class Number
    {
        private string value;

        public Number(string value)
        {
            this.value = value;
        }

        public string getValue()
        {
            return value;
        }

        static double convertFromString(string s)
        {
            double d = Double.Parse(s);
            return d;
        }

        public static Number operator +(Number a, Number b)
        {
            try
            {
                double temp = convertFromString(a.value) + convertFromString(b.value);
                return new Number(temp.ToString());
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Number operator -(Number a, Number b)
        {
            double temp = convertFromString(b.value) - convertFromString(a.value);
            return new Number(temp.ToString());
        }

        public static Number operator /(Number a, Number b)
        {
            if (a.value == "0")
            {
                return new Number("ERR: divide by 0 is prohibited");
            }
            else
            {
                double temp = convertFromString(b.value) / convertFromString(a.value);
                return new Number(temp.ToString());
            }
        }

        public static Number operator *(Number a, Number b)
        {
            double temp = convertFromString(a.value) * convertFromString(b.value);
            return new Number(temp.ToString());
        }

        public static Number operator ^(Number a, Number b)
        {
            double temp = Math.Pow(convertFromString(b.value), convertFromString(a.value));
            return new Number(temp.ToString());
        }

        public Number sqrt()
        {
            double temp = Math.Sqrt(convertFromString(value));
            value = temp.ToString();
            return this;
        }

        public Number inv()
        {
            double temp = convertFromString(value);
            if (temp != 0)
            {
                temp = -temp;
                value = temp.ToString();
            }
            return this;
        }
    }
}