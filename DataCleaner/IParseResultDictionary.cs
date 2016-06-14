using System.Collections.Generic;
using DataClean.Contracts.Interfaces;


namespace DataClean
{
    public interface IParseResultDictionary
    {
        string DictionaryFileName { get; set; }

        IParseResult[] GetAllFatalErrors();
        IParseResult[] GetAllInfoMessages();
        IParseResult[] GetAllinfoMessages();
        IParseResult[] GetAllMessages();
        IParseResult[] GetAllWarningMessages();
        IParseResult LookupCode(string resultcode);
        IParseResult[] LookupCodeList(IEnumerable<string> results);
    }
}