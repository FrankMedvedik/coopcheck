using System;
using System.Linq;
using DataClean.Contracts.Models;
using DataClean.Repository.Mgr;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataClean.Test
{
    [TestClass]
    public class ParseDictionaryRepositoryTests
    {
        private const string PostalCodeErrorCode = "AC01";

        [TestMethod]
        public void CanGetParseDictionaryTest()
        {
            var db = new DataCleanRespository();
            var msgDict = new ParseResultDictionary(db.GetMelissaReference());
            Assert.IsTrue(msgDict.Count > 300);

        }


        [TestMethod]
        public void CanGetParseDictionaryErrorsTest()
        {
            var db = new DataCleanRespository();
            var msgDict = new ParseResultDictionary(db.GetMelissaReference());
            Assert.IsTrue(msgDict.GetAllFatalErrors().Count() > 3);

        }


        [TestMethod]
        public void CanGetParseDictionaryWarningsTest()
        {
            var db = new DataCleanRespository();
            var msgDict = new ParseResultDictionary(db.GetMelissaReference());
            Assert.IsTrue(msgDict.GetAllWarningMessages().Count() > 3);

        }

        [TestMethod]
        public void CanGetParseDictionaryInformationalTest()
        {
            var db = new DataCleanRespository();
            var msgDict = new ParseResultDictionary(db.GetMelissaReference());
            Assert.IsTrue(msgDict.GetAllInfoMessages().Count() > 3);
        }



        [TestMethod]
        public void CanGetParseDictionaryAllMessagesTest()
        {
            var db = new DataCleanRespository();
            var msgDict = new ParseResultDictionary(db.GetMelissaReference());
            Assert.IsTrue(msgDict.GetAllMessages().Count() > 3);
        }


        [TestMethod]
        public void CanDoLookupTest()
        {
            var db = new DataCleanRespository();
            var msgDict = new ParseResultDictionary(db.GetMelissaReference());
            var c = msgDict.LookupCode(PostalCodeErrorCode);
            Assert.IsTrue(c.Code == PostalCodeErrorCode);
            Assert.IsTrue(c.LongDescription.Length > 0 );

        }

    }
}
