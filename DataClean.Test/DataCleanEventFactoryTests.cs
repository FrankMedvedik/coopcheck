using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DataClean.Contracts.Interfaces;
using DataClean.DataCleaner;
using DataClean.Models;
using DataClean.Repository.Mgr;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataClean.Test
{
    [TestClass]
    public class DataCleanEventFactoryTests
    {
      //  private DataCleaner.DataCleaner _dataCleaner;
      //private DataCleanRespository _dataCleanRepository; 
        private DataCleanEventFactory  _dataCleanEventFactory;

        private DataCleanCriteria _criteria;

        public DataCleanEventFactoryTests()
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
            Results = new List<IParseResult>(ParseResultDictionary.VALID_ADDRESS_RESULTS_LIST.ToList())
        };

        [TestMethod]
        public void ValidateGoodAddressTest()
        {
            var e = _dataCleanEventFactory.ValidateAddress(TestData.GoodAddresstoClean);
            Assert.IsTrue(e.HasBeenDataCleaned);
            Assert.IsTrue(e.Output.OkComplete);
            Assert.IsTrue(e.Output.OkMailingAddress);
            Assert.IsTrue(e.Output.OkPhone);
            Assert.IsTrue(e.Output.OkEmailAddress);
        }


        [TestMethod]
        public void CleanBadCityAndPostalCodetoCleanTest()
        {

            var e = _dataCleanEventFactory.ValidateAddress(TestData.BadCityAndPostalCodetoClean);
            Assert.IsFalse(e.Output.OkComplete);
            Assert.IsFalse(e.Output.OkMailingAddress);
            Assert.IsTrue(e.Output.OkPhone);
            Assert.IsTrue(e.Output.OkEmailAddress);
            Assert.IsFalse(e.Output.HasNewPostalCode);
        }

        [TestMethod]
        public void CleanBadPostalCodetoCleanTest()
        {

            var e = _dataCleanEventFactory.ValidateAddress(TestData.BadPostalCodetoClean);
            Assert.IsTrue(e.Output.OkComplete);
            Assert.IsTrue(e.Output.OkMailingAddress);
            Assert.IsTrue(e.Output.OkPhone);
            Assert.IsTrue(e.Output.OkEmailAddress);
            Assert.IsTrue(e.Output.HasNewPostalCode);
        }

        [TestMethod]
        public void CleanBadStreetAddressTest()
        {
            
            var e = _dataCleanEventFactory.ValidateAddress(TestData.BadStreetAddressToClean);
            Assert.IsFalse(e.Output.OkComplete);
            Assert.IsFalse(e.Output.OkMailingAddress);
            Assert.IsTrue(e.Output.OkPhone);
            Assert.IsTrue(e.Output.OkEmailAddress);

        }


        [TestMethod]
        public void CleanBadStateAddressTest()
        {
            // as long as the street address and zip code are good the city and state can be missing 

            var e = _dataCleanEventFactory.ValidateAddress(TestData.MissingStateToClean);
            Assert.IsTrue(e.Output.OkComplete);
            Assert.IsTrue(e.Output.OkMailingAddress);
            Assert.IsTrue(e.Output.OkPhone);
            Assert.IsTrue(e.Output.OkEmailAddress);


            Assert.IsTrue(e.Output.HasNewStateCode);
        }

        [TestMethod]
        public void CleanBadPhoneAddressTest()
        {
            var e = _dataCleanEventFactory.ValidateAddress(TestData.BadPhoneToClean);
            Assert.IsFalse(e.Output.OkComplete);
            Assert.IsTrue(e.Output.OkMailingAddress);
            Assert.IsFalse(e.Output.OkPhone);
            Assert.IsTrue(e.Output.OkEmailAddress);
        }

        [TestMethod]
        public void CleanBadEmailAddressTest()
        {
            var e = _dataCleanEventFactory.ValidateAddress(TestData.BadEmailToClean);
            Assert.IsFalse(e.Output.OkComplete);
            Assert.IsTrue(e.Output.OkMailingAddress);
            Assert.IsTrue(e.Output.OkPhone);
            Assert.IsFalse(e.Output.OkEmailAddress);
        }


        [TestMethod]
        public void CleanBadFirstNameTest()
        {
            var e = _dataCleanEventFactory.ValidateAddress(TestData.BadFirstNameToClean);
            Assert.IsTrue(e.Output.OkComplete);
            Assert.IsTrue(e.Output.OkMailingAddress);
            Assert.IsTrue(e.Output.OkPhone);
            Assert.IsTrue(e.Output.OkEmailAddress);

        }


        [TestMethod]
        public void CleanBadLastNameTest()
        {
            var e = _dataCleanEventFactory.ValidateAddress(TestData.BadLastNameToClean);
            Assert.IsTrue(e.Output.OkComplete);
            Assert.IsTrue(e.Output.OkMailingAddress);
            Assert.IsTrue(e.Output.OkPhone);
            Assert.IsTrue(e.Output.OkEmailAddress);
        }

        [TestMethod]
        public void CleanListTest()
        {
            List<IInputStreetAddress> l = new List<IInputStreetAddress>();
            l.Add(TestData.BadEmailToClean);        
            l.Add(TestData.BadFirstNameToClean);
            l.Add(TestData.BadLastNameToClean);
            l.Add(TestData.BadPhoneToClean);
            l.Add(TestData.GoodAddresstoClean);
            l.Add(TestData.MissingStateToClean);
            l.Add(TestData.BadPostalCodetoClean);
            l.Add(TestData.BadCityAndPostalCodetoClean);

            var e = _dataCleanEventFactory.ValidateAddresses(l);
            //Assert.IsTrue(e.Count(x => x.Output.OkComplete) == 3);
            //Assert.IsTrue(e.Count(x => x.Output.OkComplete == false) == 4);
            foreach (var v in e)
            {
                Assert.IsTrue(v.Output.ID == v.Input.ID);
            }
            Assert.IsTrue(e.Count == l.Count);
        }

    }
}
