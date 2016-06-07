using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DataClean.DataCleaner;
using DataClean.Interfaces;
using DataClean.Models;
using DataClean.Repository.Mgr;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataClean.Test
{
    [TestClass]
    public class DataCleanEventFactoryAutoFixTests
    {
      //  private DataCleaner.DataCleaner _dataCleaner;
      //private DataCleanRespository _dataCleanRepository; 
        private DataCleanEventFactory  _dataCleanEventFactory;

        private DataCleanCriteria _criteria;

        public DataCleanEventFactoryAutoFixTests()
        {
            _dataCleanEventFactory = new DataCleanEventFactory( new DataCleaner.DataCleaner(ConfigurationManager.AppSettings)
                , new DataCleanRespository(),_criteria = new DataCleanCriteria()
                                                                        {
                                                                            AutoFixAddressLine1 = false,
                                                                            AutoFixCity = false,
                                                                            AutoFixPostalCode = false,
                                                                            AutoFixState = false,
                                                                            ForceValidation = true
                                                                        });

        }


        private OutputStreetAddress _goodOutputStreetAddress = new OutputStreetAddress()
        {
            Results = ParseResultDictionary.VALID_ADDRESS_RESULTS_LIST.ToList()
        };

        [TestMethod]
        public void AutoFixOKCompleteAddressTest()
        {
            var e = _dataCleanEventFactory.ValidateAddress(TestData.OKCompleteAddresstoClean);
            Assert.IsTrue(e.HasBeenDataCleaned);
            Assert.IsTrue(e.Output.OkComplete);
            Assert.IsTrue(e.Output.OkMailingAddress);
            Assert.IsTrue(e.Output.OkPhone);
            Assert.IsFalse(e.Output.HasAutoFixes);
            Assert.IsTrue(e.Output.OkEmailAddress);
        }


        [TestMethod]
        public void AutofixPostalCodeTest()
        {
            
            var e = _dataCleanEventFactory.ValidateAddress(TestData.BadPostalCodetoClean);
            Assert.IsTrue(e.Output.OkComplete);
            Assert.IsTrue(e.Output.OkMailingAddress);
            Assert.IsTrue(e.Output.OkPhone);
            Assert.IsTrue(e.Output.OkEmailAddress);
            Assert.IsTrue(e.Output.HasNewPostalCode);
        }

        [TestMethod]
        public void AutoFixAddressLine1Test()
        {
            var e = _dataCleanEventFactory.ValidateAddress(TestData.BadStreetAddressToClean);
            Assert.IsFalse(e.Output.OkComplete);
            Assert.IsFalse(e.Output.OkMailingAddress);
            Assert.IsTrue(e.Output.OkPhone);
            Assert.IsTrue(e.Output.OkEmailAddress);
            Assert.IsTrue(e.Output.HasNewStreetAddressLine1);
        }


        [TestMethod]
        public void AutofixStateTest()
        {
       
            var e = _dataCleanEventFactory.ValidateAddress(TestData.MissingStateToClean);
            Assert.IsTrue(e.Output.OkComplete);
            Assert.IsTrue(e.Output.OkMailingAddress);
            Assert.IsTrue(e.Output.OkPhone);
            Assert.IsTrue(e.Output.OkEmailAddress);
            Assert.IsTrue(e.Output.HasNewStateCode);
        }


        [TestMethod]
        public void AutofixCityTest()
        {

            var e = _dataCleanEventFactory.ValidateAddress(TestData.MissingCityToClean);
            Assert.IsTrue(e.Output.OkComplete);
            Assert.IsTrue(e.Output.OkMailingAddress);
            Assert.IsTrue(e.Output.OkPhone);
            Assert.IsTrue(e.Output.OkEmailAddress);
            Assert.IsTrue(e.Output.HasNewCity);
        }


    }
}
