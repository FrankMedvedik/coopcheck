using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopCheck.Library;
using CoopCheck.WPF.Interfaces;

namespace CoopCheck.WPF.Models
{
   public  class BatchEditImport : IBatchEdit
    {
        public int Num { get; private set; }
        public string Date { get; set; }
        public string PayDate { get; set; }
        public decimal? Amount { get; set; }
        public int? JobNum { get; set; }
        public string Description { get; set; }
        public string Updated { get; set; }
        public string ThankYou1 { get; set; }
        public string StudyTopic { get; set; }
        public string ThankYou2 { get; set; }
        public string MarketingResearchMessage { get; set; }
        public VoucherList Vouchers { get; private set; }
        public List<VoucherImport> VoucherImports { get; set; }
    }
}
