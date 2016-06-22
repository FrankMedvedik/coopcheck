using System.Configuration;
using DataClean.Contracts.Interfaces;
using DataClean.Contracts.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataClean.Test
{
    [TestClass]
    public class DataCleanerAddressCleaningTests
    {
        private IDataCleaner _dataCleaner;

        public DataCleanerAddressCleaningTests()
        {
            _dataCleaner = new DataCleaner.DataCleaner(ConfigurationManager.AppSettings);
        }



        [TestMethod]
        public void CleanGoodAddressTest()
        {
            // clean an address and make sure the dataclean data is now
            IOutputStreetAddress results = new OutputStreetAddress();
            var b = _dataCleaner.VerifyAndCleanAddress(TestData.GoodAddresstoClean, out results);
            Assert.IsTrue(b);
            Assert.IsTrue(results.OkMailingAddress);
            Assert.IsTrue(results.OkComplete);
            Assert.IsTrue(results.OkPhone);
            Assert.IsTrue(results.OkEmailAddress);
        }

        [TestMethod]
        public void CleanBadPostalCodeAddressTest()
        {
            IOutputStreetAddress results = new OutputStreetAddress();
            var b = _dataCleaner.VerifyAndCleanAddress(TestData.BadPostalCodetoClean, out results);
            Assert.IsTrue(results.OkMailingAddress);
            Assert.IsTrue(results.HasNewPostalCode);
            Assert.IsTrue(results.OkPhone);
            Assert.IsTrue(results.OkEmailAddress);

        }

        [TestMethod]
        public void CleanBadStreetAddressTest()
        {
            IOutputStreetAddress results = new OutputStreetAddress();
            var b = _dataCleaner.VerifyAndCleanAddress(TestData.BadStreetAddressToClean, out results);
            Assert.IsFalse(b);
            Assert.IsFalse(results.OkMailingAddress);
            Assert.IsTrue(results.OkPhone);

        }


        [TestMethod]
        public void CleanBadStateAddressTest()
        {
            IOutputStreetAddress results = new OutputStreetAddress();
            var b = _dataCleaner.VerifyAndCleanAddress(TestData.MissingStateToClean, out results);
            Assert.IsTrue(b);
            Assert.IsTrue(results.OkPhone);
            Assert.IsTrue(results.OkEmailAddress);
            Assert.IsTrue(results.OkMailingAddress);
        }


        [TestMethod]
        public void CleanCityAndPostalCodeAddressTest()
        {
            IOutputStreetAddress results = new OutputStreetAddress();
            var b = _dataCleaner.VerifyAndCleanAddress(TestData.BadCityAndPostalCodetoClean, out results);
            Assert.IsFalse(b);
            Assert.IsTrue(results.OkPhone);
            Assert.IsTrue(results.OkEmailAddress);
            Assert.IsFalse(results.OkMailingAddress);
        }


        [TestMethod]
        public void CleanBadPhoneAddressTest()
        {
            IOutputStreetAddress results = new OutputStreetAddress();
            var b = _dataCleaner.VerifyAndCleanAddress(TestData.BadPhoneToClean, out results);
            Assert.IsFalse(b);
            Assert.IsFalse(results.OkPhone);
        }

        [TestMethod]
        public void CleanBadEmailAddressTest()
        {
            IOutputStreetAddress results = new OutputStreetAddress();
            var b = _dataCleaner.VerifyAndCleanAddress(TestData.BadEmailToClean, out results);
            Assert.IsFalse(b);
            Assert.IsFalse(results.OkEmailAddress);
        }


        [TestMethod]
        public void CleanBadFirstNameTest()
        {
            IOutputStreetAddress results = new OutputStreetAddress();
            var b = _dataCleaner.VerifyAndCleanAddress(TestData.BadFirstNameToClean, out results);
            Assert.IsTrue(b);
        }


        [TestMethod]
        public void CleanBadLastNameTest()
        {
            IOutputStreetAddress results = new OutputStreetAddress();
            var b = _dataCleaner.VerifyAndCleanAddress(TestData.BadLastNameToClean, out results);
            Assert.IsTrue(b);
        }

    }
}
