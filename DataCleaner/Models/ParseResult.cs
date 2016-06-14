using System;
using DataClean.Contracts.Interfaces;

namespace DataClean.Models
{
    public class ParseResult : IParseResult 
    {
        public static string AUTOFIX =>"Autofix";
        public String Code { get; set; }
        public String ShortDescription { get; set; }
        public bool AlternateAddressExists { get; set; }
        public String LongDescription { get; set; }
        public string Type { get; set; }
        public static string INFO => "Informational";
        public static string WARN => "Warning";
        public static string ERROR => "Error";

        public override string ToString()
        {
            return String.Format("Code: {0} | Message:{1} | Description: {2} | Type: {3} | Alternate Address Exists: {4}", Code, ShortDescription,
                LongDescription, Type, AlternateAddressExists);
            ;
        }
    }
}
