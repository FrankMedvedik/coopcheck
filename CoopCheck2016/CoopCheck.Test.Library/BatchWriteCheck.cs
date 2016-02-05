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
    public class BatchWriteCheckTest
    {
        [TestMethod]
        public void WriteCheckBatch()
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
            voc.Amount = 1;
            voc.EmailAddress = "dreckner@reckner.com";

            obj.Vouchers.Add(voc);


            obj = obj.Save();

            var nextCheckNum = NextCheckNumCommand.Execute(3);

            var list = WriteCheckBatchCommand.Execute(obj.Num, 3, nextCheckNum);

            var retVal = string.Empty;
            try
            {
                CommitCheckCommand.Execute(obj.Num, nextCheckNum);
            }
            catch (Csla.DataPortalException e)
            {
                retVal = e.Message;
            }
            Assert.IsTrue(retVal == string.Empty);
        }

        [TestMethod]
        public void NextCheckNum()
        {
            var nextCheckNum = NextCheckNumCommand.Execute(3);

            Assert.IsTrue(nextCheckNum > 0);
        }

        [TestMethod]
        public void WriteThroughCommitCheckBatch()
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
            voc.Amount = 1;
            voc.EmailAddress = "dreckner@reckner.com";

            obj.Vouchers.Add(voc);


            obj = obj.Save();

            var nextCheckNum = NextCheckNumCommand.Execute(3);

            var list = WriteCheckBatchCommand.Execute(obj.Num, 3, nextCheckNum);

            var retVal = string.Empty;
            try
            {
                CommitCheckCommand.Execute(obj.Num, nextCheckNum);
            }
            catch (Csla.DataPortalException e)
            {
                retVal = e.Message;
            }

            try
            {
                var l = PaymentInfoList.GetPaymentInfoListByBatch(obj.Num);
                var p = l[0];
                ClearChecksCommand.Execute(p.Id, DateTime.Now, p.Amount);
            }
            catch (Csla.DataPortalException e)
            {
                retVal = e.Message;
            }
            Assert.IsTrue(retVal == string.Empty);
        }
    }
}