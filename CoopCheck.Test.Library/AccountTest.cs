using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;



namespace CoopCheck.Library.Test
{
    [TestClass]
    public class AccountTest
    {//abc
 

        [TestMethod]
        public void GetAccount()
        {
            var obj = CoopCheck.Library.Account.GetAccount(2);
            Assert.IsNotNull(obj);
            Assert.AreEqual(2, obj.Id);
        }

        [TestMethod]
        public void GetAccountAsync()
        {
            var sync = new AutoResetEvent(false);
            Account.GetAccount(2, (o, e) =>
            {
                if (e.Error != null) Assert.Fail(e.Error.Message);

                var obj = e.Object;
                Assert.IsNotNull(obj);
                Assert.AreEqual(2, obj.Id);
                sync.Set();
            });
        }

        [TestMethod]
        public void AddAccount()
        {
            var obj = Account.NewAccount();
            obj.Name = "Test Account";
            obj.Number = "2158226220";
            obj.Description = "This is a test account and should be deleted";
            obj = obj.Save();
            Assert.IsTrue(obj.Id > 0);
            Assert.IsFalse(obj.IsNew);
            Assert.IsFalse(obj.IsDirty);
            Assert.IsFalse(obj.IsSavable);

            Account.DeleteAccount(obj.Id);
        }

        [TestMethod]
        public void UpdateAccount()
        {
            var obj = Account.NewAccount();
            obj.Name = "Test Account";
            obj.Description = "Delete Me";
            obj = obj.Save();

            obj = Account.GetAccount(obj.Id);
            obj.Name = "Change Test Account";
            obj.Description = "Don't Delete Me - but do delete me";
            obj = obj.Save();

            Assert.IsFalse(obj.IsNew);
            Assert.IsFalse(obj.IsDirty);
            Assert.IsFalse(obj.IsSavable);

            Account.DeleteAccount(obj.Id);

        }
         

    }
}
