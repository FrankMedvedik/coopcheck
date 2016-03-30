using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Voucher.Pay
{
    public class PrintCheckProgress
    {
        public decimal ProgressPercentage { get; set; }
        public int CurrentCheckNum { get; set; }
        public StatusInfo Status { get; set; }
    }
}
