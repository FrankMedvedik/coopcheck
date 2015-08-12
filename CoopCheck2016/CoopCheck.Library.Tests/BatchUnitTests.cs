using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoopCheck.Library.Tests
{
    [TestClass]
    public class BatchUnitTests
    {
            // holds id of batch we will create, update and delete
            int tBatchNum = 0;

            // insert values
            int tAmount = 100;
            string tDateTime = DateTime.Now.ToString();
            string  tDescription = "frankTest";
            int tJobNum = 20150000;
            string tPayDate = DateTime.Now.ToString();
            string tStudyTopic = "testing 123";
            string tThankYou1 = "Thank you 1";
            string tThankYou2 = "Thank you 2";

            //update values
            int cAmount = 200;
            string cDateTime = DateTime.Now.ToString();
            string cDescription = "changed frankTest";
            int cJobNum = 20151111;
            string cPayDate = DateTime.Now.ToString();
            string cStudyTopic = "Changed topic";
            string cThankYou1 = "changed Thank you 1";
            string cThankYou2 = "changed Thank you 2";


        [TestMethod]
        public void OpenBatchListGetTest()
        {
            var x = OpenBatchList.GetOpenBatchList();
            Assert.IsTrue(x.Any()); 
        }

        [TestMethod]
        public void BatchEditAddTest()
        {
            string changedtext = "Changed Thank you 1";

            var x = BatchEdit.NewBatchEdit();
            x.Amount = tAmount;
            x.Date = tDateTime;
            x.Description = tDescription;
            x.JobNum = tJobNum;
            x.PayDate = tPayDate;
            x.StudyTopic = tStudyTopic;
            x.ThankYou1 = tThankYou1;
            x.ThankYou2 = tThankYou2;
           var y =  x.Save();
            tBatchNum = y.Num;
            Assert.IsTrue(y.Num > 0);
            y.Delete();
        }

        [TestMethod]
        public void BatchEditUpdateTest()
        {
            var x = BatchEdit.NewBatchEdit();
            x.Amount = tAmount;
            x.Date = tDateTime;
            x.Description = tDescription;
            x.JobNum = tJobNum;
            x.PayDate = tPayDate;
            x.StudyTopic = tStudyTopic;
            x.ThankYou1 = tThankYou1;
            x.ThankYou2 = tThankYou2;
            var y = x.Save();
            y.Amount = cAmount;
            y.Date = cDateTime;
            y.Description = cDescription;
            y.JobNum = cJobNum;
            y.PayDate = cPayDate;
            y.StudyTopic = cStudyTopic;
            y.ThankYou1 = cThankYou1;
            y.ThankYou2 = cThankYou2;
            y.Updated = DateTime.Now.ToString();
            y.Save();

            x = BatchEdit.GetBatchEdit(y.Num);
            Assert.IsTrue(x.Amount == x.Amount);
            Assert.IsTrue(x.Date == x.Date);
            Assert.IsTrue(x.Description == x.Description);
            Assert.IsTrue(x.JobNum == x.JobNum);
            Assert.IsTrue(x.PayDate== x.PayDate);
            Assert.IsTrue(x.StudyTopic == x.StudyTopic);
            Assert.IsTrue(x.ThankYou1 == x.ThankYou1);
            Assert.IsTrue(x.ThankYou2 == x.ThankYou2);
            x.Delete();
            x.Save();
        }
        [TestMethod]
        public void BatchEditDeleteTest()
        {
            var x = BatchEdit.NewBatchEdit();
            x.Amount = tAmount;
            x.Date = tDateTime;
            x.Description = tDescription;
            x.JobNum = tJobNum;
            x.PayDate = tPayDate;
            x.StudyTopic = tStudyTopic;
            x.ThankYou1 = tThankYou1;
            x.ThankYou2 = tThankYou2;
            var y = x.Save();
            tBatchNum = y.Num;
            y.Delete();
            y.Save();
            x = BatchEdit.GetBatchEdit(tBatchNum);
            Assert.IsTrue(x.Num != tBatchNum);

        }
        [TestMethod]
        public void BatchEditPrint()
        {


        }

    }
}
