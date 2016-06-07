using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClean.Interfaces;
using DataClean.Models;
using log4net;
using Newtonsoft.Json;

namespace DataClean
{
    public class ParseResultDictionary : Dictionary<string, ParseResult>
    {

        public const string AUTOFIX_CITY_CODE = "AC03";
        public const string AUTOFIX_STATE_CODE = "AC02";
        public const string AUTOFIX_POSTAL_CODE = "AC01";
        public static readonly string[] AUTOFIX_STREET_ADDRESS_CODES = new string[] {"AC09","AC10","AC11","AC12","AC13","AC14","AC15","AC16","AC17","AC18","AC19","AC20"};
        public static readonly string AUTOFIX_STREET_ADDRESS_CODE = "AC10";
        public const string VALID_STREET_ADDRESS_CODE = "AS01";
        public const string VALID_EMAIL_ADDRESS_CODE = "ES01";
        public const string VALID_10_PHONE_CODE = "PS01";
        public const string VALID_7_PHONE_CODE = "PS02";
        public static string NEW_POSTAL_CODE = "AC01";
        public static string NEW_STATE_CODE = "AC02";
        public String DictionaryFileName { get; set; }

        public ParseResultDictionary()
        {
            DictionaryFileName = "MelissaCodes.json";
            Initialize();
        }
        public ParseResultDictionary(List<ParseResult> parseResultList)
        {
            try
            {
                foreach (var p in parseResultList)
                {
                    Add(p.Code, p);
                }
            }
            catch (Exception e)
            {

                Logger.Error(e.ToString());
                throw;
            }
        }
        public ParseResultDictionary(string codeFileName)
        {
            DictionaryFileName = codeFileName;
            Initialize();
        }

        public ParseResult[] GetAllInfoMessages()
        {
            return (from a in this where a.Value.Type == ParseResult.INFO select a.Value).OrderBy(x => x.Code).ToArray();
        }

        public ParseResult[] GetAllMessages()
        {
            return (from a in this select a.Value).OrderBy(x => x.Code).ToArray();
        }

        public ParseResult[] GetAllWarningMessages()
        {
            return (from a in this where a.Value.Type == ParseResult.WARN select a.Value).OrderBy(x => x.Code).ToArray();
        }
        public ParseResult[] GetAllinfoMessages()
        {
            return (from a in this where a.Value.Type == ParseResult.INFO select a.Value).OrderBy(x => x.Code).ToArray();
        }

        public ParseResult[] GetAllFatalErrors()
        {
            return (from a in this where a.Value.Type == ParseResult.ERROR select a.Value).OrderBy(x => x.Code).ToArray();
        }
        private static readonly ILog Logger =
            LogManager.GetLogger(typeof(ParseResultDictionary));

        public static readonly ParseResult[] VALID_ADDRESS_RESULTS_LIST =
        {
            new ParseResult() {Code = VALID_EMAIL_ADDRESS_CODE},
            new ParseResult() {Code = VALID_10_PHONE_CODE},
            new ParseResult() {Code = VALID_7_PHONE_CODE},
            new ParseResult() {Code = VALID_STREET_ADDRESS_CODE}
        };
            
        private void Initialize()
        {
            try
            {
                string json = File.ReadAllText(DictionaryFileName);
                var parseResultList = JsonConvert.DeserializeObject<List<ParseResult>>(json);
                foreach (var p in parseResultList)
                {
                    Add(p.Code, p);
                }
            }
            catch (Exception e)
            {

                Logger.Error(e.ToString());
                throw;
            }
        }

        public  ParseResult LookupCode(string resultcode)
        {
            ParseResult msg;
            if (!TryGetValue(resultcode, out msg))
                msg = new ParseResult()
                {
                    Code = resultcode,
                    Type = ParseResult.INFO,
                    ShortDescription = "Undefined",
                    LongDescription = String.Format("Code is not defined in {0}", DictionaryFileName)
                };
            return msg;
        }

        public ParseResult[] LookupCodeList(IEnumerable<string> results)
        {

            var v= results.Where(x =>x.Trim() != "").Select(LookupCode).ToArray();
            return v;
        }
    }
}
