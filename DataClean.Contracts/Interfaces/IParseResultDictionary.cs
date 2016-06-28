using System.Collections.Generic;
using DataClean.Contracts.Models;

namespace DataClean.Contracts.Interfaces
{
    public interface IParseResultDictionary
    {
        string DictionaryFileName { get; set; }

        ParseResult[] GetAllFatalErrors();
        ParseResult[] GetAllInfoMessages();
        ParseResult[] GetAllinfoMessages();
        ParseResult[] GetAllMessages();
        ParseResult[] GetAllWarningMessages();
        ParseResult LookupCode(string resultcode);
        ParseResult[] LookupCodeList(IEnumerable<string> results);
    }
}