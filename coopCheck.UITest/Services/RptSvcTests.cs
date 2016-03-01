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
    public class RptSvcTests
    {
        [TestMethod()]
        public void GetJobRptTest()
        {
            var v = RptSvc.GetJobRpt(TestData.JobRptPaymentCriteria);
            Assert.IsTrue(v != null);
            Assert.IsTrue(v.Result != null);
            Assert.IsTrue(v.Result.Count > 0);

        }

        [TestMethod()]
        public void GetBatchRptTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetPaymentsTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetBatchPaymentsTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetJobPaymentsTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetPositivePayRptTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetPaymentReconcileReportTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetOpenPaymentsTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetVoidedPaymentsTest()
        {
            throw new NotImplementedException();
        }
    }
}