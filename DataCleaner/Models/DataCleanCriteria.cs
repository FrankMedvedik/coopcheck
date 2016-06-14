using System.Collections.Generic;
using DataClean.Contracts.Interfaces;

namespace DataClean.Models
{
    public class DataCleanCriteria : IDataCleanCriteria
    {
        public bool AutoFixPostalCode { get; set; }
        public bool AutoFixState{ get; set; }
        public bool AutoFixAddressLine1 { get; set; }
        public bool AutoFixCity { get; set; }
        public bool ForceValidation { get; set; }

        public string ToFormattedString(char token)
        {
            var v = new List<KeyValuePair<string, string>>();
            string s = string.Empty;
            v.Add(new KeyValuePair<string, string>("AutoFixPostalCode", AutoFixPostalCode.ToString()));
            v.Add(new KeyValuePair<string, string>("AutoFixState", AutoFixState.ToString()));
            v.Add(new KeyValuePair<string, string>("AutoFixAddressLine1", AutoFixAddressLine1.ToString()));
            v.Add(new KeyValuePair<string, string>("AutoFixCity", AutoFixCity.ToString()));
            v.Add(new KeyValuePair<string, string>("ForceValidation", ForceValidation.ToString()));

            foreach (var a  in v)
            {
                s = string.Concat(s, token, a.Key, token, a.Value);
            }
            return s;
        }

    }
}