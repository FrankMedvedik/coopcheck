using Email1099.WPF.Services;

namespace Email1099.WPF.Models
{
    public partial class vwPayment
    {
        public string pay_date_exp
        {
            get { return string.Format((string) "{0:d}", (object) pay_date); }
        }

        public string phone_number_exp
        {
            get { return PhoneNumberFormattingSvc.FormatPhoneNumber(phone_number); }
        }

        public string cleared_date_exp
        {
            get { return string.Format((string) "{0:d}", (object) cleared_date); }
        }

        public string tran_amount_exp
        {
            get
            {
                return string.Format((string) "{0:C0}", (object) tran_amount);
            }
        }
    }
}
