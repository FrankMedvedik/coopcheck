using System.Collections.Generic;

namespace DataClean.Contracts.Interfaces
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