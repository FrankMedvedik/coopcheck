using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoopCheck.Library;

namespace CoopCheck.Test.Library
{
    [TestClass]
    public class BatchEditTest
    {
        [TestMethod]
        public void GetBatch()
        {
            var obj = BatchEdit.GetBatchEdit(100);
            Assert.IsNotNull(obj);
            Assert.AreEqual(100, obj.Num);
        }

        [TestMethod]
        public void GetBatchAsync()
        {
            var sync = new AutoResetEvent(false);
            BatchEdit.GetBatchEdit(100, (o, e) =>
            {
                if (e.Error != null) Assert.Fail(e.Error.Message);
                var obj = e.Object;
                Assert.IsNotNull(obj);
                Assert.AreEqual(100, obj.Num);
                sync.Set();
            });
            sync.WaitOne(1000);
        }

        [TestMethod]
        public void AddBatchVoucher()
        {
            var obj = BatchEdit.NewBatchEdit();
            obj.Amount = 100;
            obj.Date = DateTime.Now.ToShortDateString();
            obj.Description = "Please delete me!";
            obj.JobNum = 99999999;
            obj.MarketingResearchMessage = "Thanks";
            obj.PayDate = DateTime.Now.ToShortDateString();
            obj.StudyTopic = "Survey";
            obj.ThankYou1 = "Thank You";
            obj.ThankYou2 = "Thank You Thank You";

             var voc = VoucherEdit.NewVoucherEdit();
            voc.First = "John";
            voc.Last = "Public";
            voc.AddressLine1 = "1600 Manor Drive";
            voc.Municipality = "Chalfont";
            voc.Region = "PA";
            voc.PostalCode = "18914";
            voc.Amount = 100;

            obj.Vouchers.Add(voc);

            
            obj = obj.Save();

            obj = BatchEdit.GetBatchEdit(obj.Num);

            Assert.IsTrue(obj.Vouchers.Count > 0);
            Assert.AreEqual("Public", obj.Vouchers[0].Last);

            BatchEdit.DeleteBatchEdit(obj.Num);

        }

        [TestMethod]
        public void BadAddressVoucherTest()
        {
            var obj = BatchEdit.NewBatchEdit();
            obj.Amount = 100;
            obj.Date = DateTime.Now.ToShortDateString();
            obj.Description = "Please delete me!";
            obj.JobNum = 99999999;
            obj.MarketingResearchMessage = "Thanks";
            obj.PayDate = DateTime.Now.ToShortDateString();
            obj.StudyTopic = "Survey";
            obj.ThankYou1 = "Thank You";
            obj.ThankYou2 = "Thank You Thank You";

            var voc = VoucherEdit.NewVoucherEdit();
            voc.First = "John";
            voc.Last = "Public";
            voc.AddressLine1 = "Your Street";
            voc.Municipality = "Anytown";
            voc.Region = "PA";
            voc.PostalCode = "18914";
            voc.Amount = 100;

            obj.Vouchers.Add(voc);

            Assert.IsFalse(obj.IsValid);
            
        }
        [TestMethod]
        public void UpdateVoucherTest()
        {
            var obj = BatchEdit.NewBatchEdit();
            obj.Amount = 100;
            obj.Date = DateTime.Now.ToShortDateString();
            obj.Description = "Please delete me!";
            obj.JobNum = 99999999;
            obj.MarketingResearchMessage = "Thanks";
            obj.PayDate = DateTime.Now.ToShortDateString();
            obj.StudyTopic = "Survey";
            obj.ThankYou1 = "Thank You";
            obj.ThankYou2 = "Thank You Thank You";

            var voc = VoucherEdit.NewVoucherEdit();
            voc.First = "John";
            voc.Last = "Public";
            voc.AddressLine1 = "1600 Manor Drive";
            voc.Municipality = "Chalfont";
            voc.Region = "PA";
            voc.PostalCode = "18914";
            voc.Amount = 100;

            obj.Vouchers.Add(voc);


            obj = obj.Save();

            obj = BatchEdit.GetBatchEdit(obj.Num);

            obj.Vouchers[0].First = "Jim";

            obj = obj.Save();

            obj = BatchEdit.GetBatchEdit(obj.Num);

            Assert.IsTrue(obj.Vouchers[0].First == "Jim");
            BatchEdit.DeleteBatchEdit(obj.Num);
        }
    }
}
