using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoopCheck.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClean.Models;

namespace CoopCheck.WPF.Services.Tests
{
    [TestClass()]
    public class DataCleanSvcTests
    {
        [TestMethod()]
        public void ValidateAddressesTest()
        {

            var a = DataCleanSvc.ValidateAddresses(new List<InputStreetAddress>());
        }
    }
}