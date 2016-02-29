using System;
using System.Collections.Generic;
using DataClean.Models;
using DataClean.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataClean.Web.Tests.Controllers
{
    [TestClass()]
    public class DataCleanControllerTests
    {
        [TestMethod()]
        public void DataCleanControllerTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetTest()
        {
            List<InputStreetAddress> l = new List<InputStreetAddress>();
            l.Add(TestData.GoodAddresstoClean);
            var controller = new DataCleanController();
            var result = controller.Get(l);
            Assert.AreEqual(result.StatusCode, System.Net.HttpStatusCode.OK);

        }

    }
}