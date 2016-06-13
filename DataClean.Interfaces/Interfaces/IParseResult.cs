using System;

namespace DataClean.Interfaces
{
    public interface IParseResult
    {
        String Code { get; set; }
        String ShortDescription { get; set; }
        bool AlternateAddressExists { get; set; }
        String LongDescription { get; set; }
        string Type { get; set; }
        string ToString();
    }
}