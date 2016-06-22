
using CoopCheck.Repository.Services;

namespace CoopCheck.Repository
{
    using System;

    public partial class vwPayment
    {
        public string pay_date_exp
        {
            get { return string.Format("{0:d}", pay_date); }
        }

        public string phone_number_exp
        {
            get { return PhoneNumberFormattingSvc.FormatPhoneNumber(phone_number); }
        }

        public string cleared_date_exp
        {
            get { return string.Format("{0:d}", cleared_date); }
        }

        public string tran_amount_exp
        {
            get
            {
                return string.Format("{0:C0}", tran_amount);
            }
        }

        public bool IsSwiftPayment
        {
            get { return account_id == 8; }
        }
        public bool IsCheck
        {
            get { return account_id != 8; }
        }
    }
}
