using System;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace coopCheck.UITest.Services
{
    [TestClass()]
    public class BankAccountSvcTests
    {
        [TestMethod()]
        public void GetAccountsTest()
        {
            var v = BankAccountSvc.GetAccounts();
            Assert.AreNotEqual(v.Result, null);
            Assert.AreNotEqual(v.Result.Count, 0);
        }

        [TestMethod()]
        public void NextCheckNumTest()
        {
            var v = BankAccountSvc.NextCheckNum(TestData.GoodAccountNum);
            Assert.AreNotEqual(v, null);
            Assert.AreNotEqual(v, 0);
        }
    }

    public class TestData
    {
        public static int GoodAccountNum = 3;
        public static int GoodBatchEditID = 57;

        public static string ReaderUser
        {
            get { return Environment.UserName; }
        }

        public static string BadUser
        {
            get { return Environment.UserName; }
        }

        public static string WriterUser
        {
            get { return Environment.UserName; }
        }

        public static PaymentReportCriteria JobRptPaymentCriteria { get; set; }
    }
}
