using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.DesignData
{
    public static class BatchEditDesign
    {
        public static BatchEditImport LoadTestData()
        {
            // holds id of batch we will create, update and delete
       

            // insert values
            int tAmount = 100;
            string tDateTime = DateTime.Now.ToString();
            string tDescription = "frankTest";
            int tJobNum = 20150000;
            string tPayDate = DateTime.Now.ToString();
            string tStudyTopic = "testing 123";
            string tThankYou1 = "Thank you 1";
            string tThankYou2 = "Thank you 2";

            var x = new BatchEditImport();
            x.Amount = tAmount;
            x.Date = tDateTime;
            x.Description = tDescription;
            x.JobNum = tJobNum;
            x.PayDate = tPayDate;
            x.StudyTopic = tStudyTopic;
            x.ThankYou1 = tThankYou1;
            x.ThankYou2 = tThankYou2;
            return x;

        }


    }
}
