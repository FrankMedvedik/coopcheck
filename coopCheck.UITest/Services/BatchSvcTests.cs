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
    public class BatchSvcTests
    {
        [TestMethod()]
        public void GetBatchEditAsyncTest()
        {
            var a =  BatchSvc.GetBatchEditAsync(TestData.GoodBatchEditID);
            Assert.AreNotEqual(a.Result, null);
        }

        [TestMethod()]
        public void GetNewBatchEditAsyncTest()
        {
            var a = BatchSvc.GetNewBatchEditAsync();
            Assert.AreNotEqual(a.Result, null);
        }

        [TestMethod()]
        public void DeleteBatchEditAsyncTest()
        {
            var a = BatchSvc.GetNewBatchEditAsync();
            var b = a.Result;
            Assert.AreNotEqual(b, null);
            BatchSvc.DeleteBatchEditAsync(b.Num);
            var x = BatchSvc.GetBatchEditAsync(b.Num);
            Assert.AreNotEqual(x.Result.Num, b.Num);
        }

        [TestMethod()]
        public void NextCheckNumTest()
        {
            var v = BatchSvc.NextCheckNum(TestData.GoodAccountNum);
            Assert.AreNotEqual(v, null);
            Assert.AreNotEqual(v, 0);
        }

    }
}