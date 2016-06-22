using System;
using System.Collections.Generic;
using System.Linq;
using DataClean.Contracts.Interfaces;
using DataClean.Contracts.Models;
using DataClean.Repository.Mgr;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataClean.Test
{
    [TestClass]
    public class DataCleanEventRespositoryTests
    {
     
        private OutputStreetAddress _goodOutputStreetAddress = new OutputStreetAddress()
        {
            Results = new List<IParseResult>(ParseResultDictionary.VALID_ADDRESS_RESULTS_LIST.ToList())
        };

        [TestMethod]
        public void CanSaveEventTest()
        {
            var testTime = DateTime.Now;
            var db = new DataCleanRespository();
            var e = new DataCleanEvent()
            {
                Input = TestData.GoodAddresstoClean,
                DataCleanDate = testTime,
                Output = _goodOutputStreetAddress

            };
            // if the output id does not match it will not resolve correctly when we combine the two after datacleaner is done
            //
            e.Output.ID = e.ID;

            var id = e.ID;
            db.SaveEvent(e);
            var d = db.GetEvent(e.ID);
            Assert.IsTrue(d.ID == e.ID);
            Assert.IsTrue(d.Output.ID == e.ID);
            Assert.IsTrue(d.Output.ID == e.Input.ID);
            Assert.IsTrue(d.DataCleanDate == testTime);
        }
        [TestMethod]
        public void CantSaveUncleanEventTest()
        {
            var db = new DataCleanRespository();
            var e = new DataCleanEvent()
            {
                Input = TestData.GoodAddresstoClean
                //DataCleanDate = DateTime.Now  

               // We dont want to save an event without a data clean date! 
            };
            var id = e.ID;
            try
            {
                db.SaveEvent(e);
            }
            catch( Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("date"));
            }
        }

        [TestMethod]
        public void CantSaveOutputDoesNotMatchInputTest()
        {
            var db = new DataCleanRespository();
            var e = new DataCleanEvent()
            {
                Input = TestData.GoodAddresstoClean,
                DataCleanDate = DateTime.Now,
                 Output = _goodOutputStreetAddress
            };
            // output id does not match input
            var id = e.ID;
            try
            {
                db.SaveEvent(e);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("id"));
            }
        }

        [TestMethod]
        public void CantSaveEmptyEventTest()
        {
            var db = new DataCleanRespository();
            var e = new DataCleanEvent();
            var id = e.ID;
            try
            {
                db.SaveEvent(e);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("event"));
            }
        }


        [TestMethod]
        public void CantSaveWithoutInputTest()
        {
            var db = new DataCleanRespository();
            var e = new DataCleanEvent()
            {
                DataCleanDate = DateTime.Now,
                Output = new OutputStreetAddress()
                 
            };
            var id = e.ID;
            try
            {
                 
                db.SaveEvent(e);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("input"));
            }
        }
        [TestMethod]
        public void CantSaveWithoutOutputTest()
        {
            var db = new DataCleanRespository();
            var e = new DataCleanEvent()
            {
                DataCleanDate = DateTime.Now,
                Input = TestData.GoodAddresstoClean
            };
            var id = e.ID;
            try
            {

                db.SaveEvent(e);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("output"));
            }
        }

    }
}
