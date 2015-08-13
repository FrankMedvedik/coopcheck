using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopCheck.Library
{
    internal class Util
    {
        public static string AddSpace(string input)
        {
            string retVal = string.Empty;
            if (input.Length > 0)
            {
                retVal = input + " ";
            }
            return retVal;
        }
    }
}
