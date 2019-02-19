using System;
using System.Linq;
using System.Text;

namespace Clouder.Server.Api.Extension
{
    public class ToFarsiNumeric
    {
        public static string Convert(object value, string parameter)
        {
            if (value == null)
            {
                return null;
            }

            var before = "";
            var after = "";
            var code = "";
            var numberText = value.ToString();
            double number = 0;
            var isNumber = double.TryParse(numberText, out number);

            if (parameter != null)
            {
                var parameterStr = parameter.ToString();
                before = new string(parameterStr.TakeWhile(ch => !Char.IsLetter(ch)).ToArray());
                code = new string(parameterStr.Skip(before.Count()).TakeWhile(ch => Char.IsLetter(ch)).ToArray());
                after = parameterStr.Substring(before.Length + code.Length);
            }
            //if (code.Contains("M")) //Multiple Numbers
            //{
            //    var values = (value as IEnumerable<string>).Select(n => $"{before}{ToFarsi(n)}{after}").ToList();
            //    return values;
            //}

            if (code.Contains("N")) //Accounting Number
            {
                numberText = InsertComma(numberText);
            }

            var converted = ToFarsi(numberText);

            if (code.Contains("Z")) //Don't show anything if zero
            {
                if ((int)value == 0)
                {
                    return "";
                }
            }

            if (isNumber && code.Contains("S")) //Signed Number
            {
                if (Math.Sign(number) == 1)
                {
                    converted = "+" + converted;
                }
            }

            return before + converted + after;
        }

        private static string ToFarsi(string number)
        {
            var convertedChars = new StringBuilder();
            char[] convertedChar = new char[1];
            byte[] bytes = new byte[] { 217, 160 };
            char[] inputCharArray = number.ToCharArray();
            var utf8Encoder = new UTF8Encoding();
            var utf8Decoder = utf8Encoder.GetDecoder();

            foreach (char c in number)
            {
                if (char.IsDigit(c))
                {
                    bytes[1] = System.Convert.ToByte(160 + char.GetNumericValue(c));
                    utf8Decoder.GetChars(bytes, 0, 2, convertedChar, 0);
                    convertedChars.Append(convertedChar[0]);
                }
                else
                {
                    convertedChars.Append(c);
                }
            }

            return convertedChars.ToString();
        }

        private static string InsertComma(string number)
        {
            for (int i = number.Length - 3; i > 0; i -= 3)
            {
                if (i > 0)
                {
                    number = number.Insert(i, ",");
                    i--;
                }
            }

            return number;
        }
    }
}
