using System.Collections.Generic;
using System.Threading.Tasks;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Services
{
    public static class PaymentMethodSvc
    {
        public async static Task<List<PaymentMethod>> GetMethods()
        {
            var plist = new List<PaymentMethod>();

                plist.Add(new PaymentMethod()
                {

                    Key = "Check",
                    Value = "Check"
                });
                plist.Add(new PaymentMethod()
                {
                    Key = "SwiftPay",
                    Value = "Swift Pay"
                });

            return plist;
        }
    }
}