using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoopCheck.Library.Tests
{
    [TestClass]
    public class AccountUnitTests
    {
        [TestMethod]
        public void AccountListGetTest()
        {
            var x = AccountList.GetAccountList();
            Assert.IsTrue(x.Any()); 
        }
        [TestMethod]
        public void OpenBatchListGetTest()
        {
            var x = OpenBatchList.GetOpenBatchList();
            Assert.IsTrue(x.Any());
        }
        [TestMethod]
        public void AccountGetTest()
        {
            var x = Account.GetAccount(2);
            Assert.IsTrue(x.Name == "Honoraria");
        }
        [TestMethod]
        public void AccountAddTest()
        {
            var x = Account.NewAccount();
        
            x.Name = "test";
            x.Number = "6666";
            x.Balance = 0;
            x.Description = "test";
            x.Save();
            Assert.IsTrue(x.Id > 0);
            x.Delete();
            Assert.IsTrue(x.Id > 0);
        }

    }
}
