using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToasoanTTXVN.Quanlynhanbut
{
    public class Number2Text
    {
        private bool m_IsVND = true;

        public bool IsVND
        {
            set { this.m_IsVND = value; }
            get { return this.m_IsVND; }

        }

        public string OneDigit(string pNumber)
        {
            switch (pNumber)
            {
                case "0": return "không";
                case "1": return "một";
                case "2": return "hai";
                case "3": return "ba";
                case "4": return "bốn";
                case "5": return "năm";
                case "6": return "sáu";
                case "7": return "bảy";
                case "8": return "tám";
                case "9": return "chín";
            }
            return "không";
        }

        public string TwoDigit(string pNumber)
        {
            string num2text = "";

            string first_digit = pNumber.Substring(0, 1);
            string second_digit = pNumber.Substring(1, 1);

            if (first_digit == "1")
                num2text = Space("mười");
            else
            {
                num2text = Space(OneDigit(first_digit)) + Space("mươi");
            }

            if (second_digit == "1")
            {
                if (first_digit == "1")
                    num2text += Space("một");
                else
                    num2text += Space("mốt");
            }
            else if (second_digit == "5")
            {
                num2text += Space("lăm");
            }
            else
            {
                if (second_digit != "0")
                    num2text += Space(OneDigit(second_digit));
            }
            return num2text;
        }
        public string ThreeDigit(string pNumber)
        {

            if (pNumber == "000")
                return "";

            string num2text = "";
            string first_digit = pNumber.Substring(0, 1);
            string second_digit = pNumber.Substring(1, 1);
            string third_digit = pNumber.Substring(2, 1);

            num2text = Space(OneDigit(first_digit)) + Space("trăm");

            if (second_digit == "0")
            {
                if (third_digit != "0")
                {
                    num2text += Space("lẻ");
                    num2text += Space(OneDigit(third_digit));
                }
            }
            else
            {
                num2text += TwoDigit(second_digit + third_digit);
            }

            return num2text;
        }
        private string Space(string s)
        {
            return s + " ";
        }
        private string UnitDigit(int u)
        {
            switch (u)
            {
                case 0: return (this.IsVND == true ? "đồng chẵn." : "đô la");
                case 1: return "nghìn";
                case 2: return "triệu";
                case 3: return "tỷ";
            }
            return "cent";
        }

        public string ConvertNumber(string pNumber)
        {
            string tempNumber = "";
            string[] arrNumber = null;
            string strCent = "";
            if (this.IsVND == true)
            {
                tempNumber = string.Format("{0:0,0}", Convert.ToDouble(pNumber));
                arrNumber = tempNumber.Split(',');
            }
            else
            {
                tempNumber = string.Format("{0:0,0.00}", Convert.ToDouble(pNumber));
                string[] arrTemp = tempNumber.Split('.');

                arrNumber = arrTemp[0].Split(',');
                strCent = arrTemp[1];

            }

            string num2text = "";

            for (int i = arrNumber.Length - 1; i >= 0; i--)
            {
                int len = arrNumber[i].Length;
                string temp = "";
                switch (len)
                {
                    case 1:
                        temp = Space(OneDigit(arrNumber[i]));
                        break;
                    case 2:
                        temp = TwoDigit(arrNumber[i]);
                        break;
                    case 3:
                        temp = ThreeDigit(arrNumber[i]);
                        break;
                }

                int j = arrNumber.Length - i - 1;
                if (temp != "" || (temp == "" && j == 0))
                    temp += Space(UnitDigit(j));

                num2text = temp + num2text;
            }

            if (strCent != "00" && this.m_IsVND == false)
            {
                int centNumber = Convert.ToInt32(strCent);
                strCent = centNumber.ToString();
                num2text = num2text.TrimEnd();
                switch (strCent.Length)
                {
                    case 1:
                        num2text += ", " + Space(OneDigit(strCent));
                        break;
                    case 2:
                        num2text += ", " + TwoDigit(strCent);
                        break;
                }

                num2text += Space(UnitDigit(-1));
            }
            return num2text.TrimEnd();
        }
    }
}
