using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEb.API2.Owin.NinJect
{
    public interface IValuesProvider
    {
        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <returns>The values</returns>
        IEnumerable<string> GetValues();
    }
    public class ValuesProvider : IValuesProvider, IDisposable
    {
        private int value = 1;

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <returns>The values</returns>
        public IEnumerable<string> GetValues()
        {
            yield return "Value99999" + this.value++;
            yield return "Value" + this.value++;
            yield return "Value" + this.value++;
        }

        public void Dispose()
        {
        }
    }
}