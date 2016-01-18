using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClean.Models;

namespace CoopCheck.WPF.Models
{
    public class VoucherExcelExport
    {
        public string First { get; set; }
        public string Last { get; set; }
        public string Company { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Municipality { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string JobNumber { get; set; }
        public decimal Amount { get; set; }
        public Boolean OkComplete { get; set; }
        public Boolean OkMailingAddress { get; set; }
        public Boolean OkEmailAddress { get; set; }
        public Boolean OkPhone { get; set; }
        public string SuggestedAddress { get; set; }
    }
}
