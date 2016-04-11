using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Email1099.WPF.Models;

namespace Email1099.WPF.Content.Design
{
    public static  class MockPayments
    {
        public static List<vwPayment> GetPayments()
        {
             vwPayment a = new vwPayment
             {
                 email = "fmedvedik+mockpayment@gmail.com",
                 job_num = 20150299,
                 first_name = "frak",
                 last_name = "medvediky",
                 phone_number = "2672550067"
             };
        vwPayment b = new vwPayment
        {
            email = "fmedvedik+mockpayment2@gmail.com",
            job_num = 20150299,
            first_name = "caren",
            last_name = "medvediky",
            phone_number = "2672550067"
        };
         return new List<vwPayment>() {a,b};
        }
    }

}
