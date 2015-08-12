using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoopCheck.Library.Tests
{
    [TestClass]
    public class VoucherUnitTests
    {
        int tBatchNum = 22897;
        
        [TestMethod]
        public void BatchVoucherListGetTest()
        {

            
            var x = BatchEdit.GetBatchEdit(tBatchNum);
            Assert.IsTrue(x.Vouchers.Any()); 
        }
        [TestMethod]
        public void AddVoucherTest()
        {
            var b = BatchEdit.GetBatchEdit(tBatchNum);
            int priorCnt = b.Vouchers.Count();

            var v = VoucherEdit.NewVoucherEdit();
            v.AddressLine1 = "addr1 test";
            v.AddressLine2 = "addr2 test";
            v.Amount = 100;
            //v.Company = "testCompany";
            v.Country = "US";
            v.EmailAddress = "heywood@nasa.gov";
            v.First = "testFirstName";
            v.Last = "testLastName";
            //v.Middle = "testMiddleName";
            v.Municipality = "test City";
            //v.NamePrefix = "tst";
            v.PhoneNumber ="5551212555";
            v.PostalCode = "19010";
            v.Region = "PA";
//            v.Suffix = "sfx";
  //          v.Title = "ttl";
            v.ApplyEdit();
            foreach (var i in v.BrokenRulesCollection)
                Assert.Fail(i.ToString());

            b.Vouchers.Add(v);
            b.ApplyEdit();
            try
            {
                var B = b.Save();
                Assert.IsTrue(priorCnt < B.Vouchers.Count());
            }
            catch (Csla.DataPortalException e)
            {
                foreach (var i in b.BrokenRulesCollection)
                    Assert.Fail(i.ToString());
                Assert.Fail(e.BusinessException.ToString());
            }
            catch (Exception e)
            {
                foreach(var i in b.BrokenRulesCollection)
                    Assert.Fail(i.ToString());

                Assert.Fail(e.ToString());
            }
            

        }


    }
}
