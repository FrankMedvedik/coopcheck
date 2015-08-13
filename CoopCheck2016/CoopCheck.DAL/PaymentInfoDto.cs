using System;
using Csla;

namespace CoopCheck.DAL
{
    public class PaymentInfoDto
    {
        public int Id { get; set; }
        public int BatchNum { get; set; }
        public SmartDate PayDate { get; set; }
        public int JobNum { get; set; }
        public string ThankYou1 { get; set; }
        public string StudyTopic { get; set; }
        public string ThankYou2 { get; set; }
        public string MarketingResearchMessage { get; set; }
        public SmartDate CheckDate { get; set; }
        public string CheckNum { get;set;}
        public decimal Amount { get; set; }
        public string PersonId { get; set; }
        public string Prefix {get;set;} 
        public string FirstName {get;set;} 
        public string MiddleName {get;set;} 
        public string LastName {get;set;}
        public string NameSuffix {get;set;}
        public string Title {get;set;}
        public string Company {get;set;}
        public string Address1 {get;set;} 
        public string Address2 {get;set;} 
        public string Municipality {get;set;}
        public string Region {get;set;}
        public string PostalCode {get;set;}
        public string Country {get;set;}
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Completed { get; set; }
    }
}
