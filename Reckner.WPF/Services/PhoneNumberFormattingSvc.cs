using System;
using System.Text;

namespace Reckner.WPF.Services
{
    public static  class PhoneNumberFormattingSvc
    {
        public static string FormatPhoneNumber(string phoneNum)
        {
            var p = StripPhoneNumber(phoneNum);
            if (p.Length != 10)
                return p;
            else
                return String.Format(String.Format("({0}) {1}-{2}", p.Substring(0, 3), p.Substring(3, 3), p.Substring(6)));

        }
        public static String StripPhoneNumber(string phoneNumber)
        {
            {
                if (phoneNumber == null)
                    return "";
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (char c in phoneNumber)
                    {
                        if (c >= '0' && c <= '9')
                        { sb.Append(c); }
                    }
                    return sb.ToString();
                }
            }
        }
    }
}