﻿
namespace CoopCheck.Reports.Contracts.Models
{
    public class CoopCheckClient 
    {
        public string Address { get; set; }
        public string Attention { get; set; }
        public string City { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public bool? inSolomon { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}