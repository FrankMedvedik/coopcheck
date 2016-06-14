using System;
using System.Collections.Generic;
using DataClean.Contracts.Interfaces;

namespace CoopCheck.WPF.Models
{
    public class CleanedAddress : IOutputStreetAddress
    {
        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        public string aaaResultsAsString { get; }
        public string AddressDeliveryInstallation { get; set; }
        public string AddressExtras { get; set; }
        public string AddressHouseNumber { get; set; }
        public string AddressKey { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLockBox { get; set; }
        public string AddressPostDirection { get; set; }
        public string AddressPreDirection { get; set; }
        public string AddressPrivateMailboxName { get; set; }
        public string AddressPrivateMailboxRange { get; set; }
        public string AddressRouteService { get; set; }
        public string AddressStreetName { get; set; }
        public string AddressStreetSuffix { get; set; }
        public string AddressSuiteName { get; set; }
        public string AddressSuiteNumber { get; set; }
        public string AddressTypeCode { get; set; }
        public string AreaCode { get; set; }
        public List<IParseResult> AutoFixes { get; }
        public string CarrierRoute { get; set; }
        public string CBSACode { get; set; }
        public string CBSADivisionCode { get; set; }
        public string CBSADivisionLevel { get; set; }
        public string CBSADivisionTitle { get; set; }
        public string CBSALevel { get; set; }
        public string CBSATitle { get; set; }
        public string CensusBlock { get; set; }
        public string CensusTract { get; set; }
        public string City { get; set; }
        public string CityAbbreviation { get; set; }
        public string CompanyName { get; set; }
        public string CongressionalDistrict { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string CountyFIPS { get; set; }
        public string CountyName { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfDeath { get; set; }
        public string DeliveryIndicator { get; set; }
        public string DeliveryPointCheckDigit { get; set; }
        public string DeliveryPointCode { get; set; }
        public string DemographicsGender { get; set; }
        public string DemographicsResults { get; set; }
        public string DomainName { get; set; }
        public string EmailAddress { get; set; }
        public string Error { get; }
        public List<IParseResult> Errors { get; }
        public string Gender { get; set; }
        public string Gender2 { get; set; }
        public bool HasAutoFixes { get; }
        public bool HasNewCity { get; }
        public bool HasNewPostalCode { get; }
        public bool HasNewStateCode { get; }
        public bool HasNewStreetAddressLine1 { get; }
        public string HouseholdIncome { get; set; }
        public int ID { get; set; }
        public List<IParseResult> Informational { get; }
        public string Latitude { get; set; }
        public string LengthOfResidence { get; set; }
        public string Longitude { get; set; }
        public string MailboxName { get; set; }
        public string MaritalStatus { get; set; }
        public string NameFirst { get; set; }
        public string NameFirst2 { get; set; }
        public string NameFull { get; set; }
        public string NameLast { get; set; }
        public string NameLast2 { get; set; }
        public string NameMiddle { get; set; }
        public string NameMiddle2 { get; set; }
        public string NamePrefix { get; set; }
        public string NamePrefix2 { get; set; }
        public string NameSuffix { get; set; }
        public string NameSuffix2 { get; set; }
        public string NewAreaCode { get; set; }
        public string Occupation { get; set; }
        public bool OkComplete { get; }
        public bool OkEmailAddress { get; }
        public bool OkMailingAddress { get; }
        public bool OkPhone { get; }
        public string OwnRent { get; set; }
        public string ParseResults { get; }
        public string PhoneExtension { get; set; }
        public string PhoneNumber { get; set; }
        public string PhonePrefix { get; set; }
        public string PhoneSuffix { get; set; }
        public string PlaceCode { get; set; }
        public string PlaceName { get; set; }
        public string Plus4 { get; set; }
        public string PostalCode { get; set; }
        public string PresenceOfChildren { get; set; }
        public string PrivateMailBox { get; set; }
        public string RecordExtras { get; set; }
        public string RecordID { get; set; }
        public List<IParseResult> Results { get; set; }
        public string Salutation { get; set; }
        public string State { get; set; }
        public string StateName { get; set; }
        public string Suite { get; set; }
        public string TopLevelDomain { get; set; }
        public string UrbanizationName { get; set; }
        public string UTC { get; set; }
        public string Warning { get; }
        public List<IParseResult> Warnings { get; }
        public string SuggestedAddress()
        {
            throw new NotImplementedException();
        }
    }
}
