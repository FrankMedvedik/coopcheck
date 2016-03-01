using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoopCheck.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coopCheck.UITest.Services;

namespace CoopCheck.WPF.Services.Tests
{
    [TestClass()]
    public class UserAuthSvcTests
    {
        [TestMethod()]
        public void CanReadTest()
        {
            Assert.AreEqual(UserAuthSvc.CanRead(TestData.ReaderUser), true);
            Assert.AreEqual(UserAuthSvc.CanRead(TestData.BadUser), false);
        }

        [TestMethod()]
        public void CanWriteTest()
        {
            Assert.AreEqual(UserAuthSvc.CanWrite(TestData.WriterUser), true);
            Assert.AreEqual(UserAuthSvc.CanWrite(TestData.BadUser), false);
        }
    }
}