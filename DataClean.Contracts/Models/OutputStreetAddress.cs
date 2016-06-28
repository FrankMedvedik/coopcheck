using System;
using System.Collections.Generic;
using System.Linq;
using DataClean.Contracts.Interfaces;

namespace DataClean.Contracts.Models
{
    public class OutputStreetAddress : IOutputStreetAddress
    {
        public string RecordID { get; set; }
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
        public string CBSACode { get; set; }
        public string CBSADivisionCode { get; set; }
        public string CBSADivisionLevel { get; set; }
        public string CBSADivisionTitle { get; set; }
        public string CBSALevel { get; set; }
        public string CBSATitle { get; set; }
        public string CarrierRoute { get; set; }
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
        public string Gender { get; set; }
        public string Gender2 { get; set; }
        public string HouseholdIncome { get; set; }
        public int ID { get; set; }
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
        public string OwnRent { get; set; }
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
        public List<ParseResult> Results { get; set; }

        public string aaaResultsAsString
        {
            get { return Results.Aggregate("", (current, r) => current + (r.ToString() + Environment.NewLine)); }
        }

        public string Salutation { get; set; }
        public string State { get; set; }
        public string StateName { get; set; }
        public string Suite { get; set; }
        public string TopLevelDomain { get; set; }
        public string UTC { get; set; }
        public string UrbanizationName { get; set; }

        public Boolean OkComplete
        {
            get { return OkMailingAddress && OkEmailAddress && OkPhone; }
        }

        public Boolean OkMailingAddress
        {
            get { return (Results.Any(x => x.Code == ParseResultDictionary.VALID_STREET_ADDRESS_CODE)); }
        }

        public Boolean HasNewPostalCode
        {
            get { return (Results.Any(x => x.Code == ParseResultDictionary.NEW_POSTAL_CODE)); }
        }

        public Boolean HasNewStateCode
        {
            get { return (Results.Any(x => x.Code == ParseResultDictionary.NEW_STATE_CODE)); }
        }

        public Boolean OkEmailAddress
        {
            get { return (Results.Any(x => x.Code == ParseResultDictionary.VALID_EMAIL_ADDRESS_CODE)); }
        }

        public Boolean OkPhone
        {
            get
            {
                return
                    (Results.Any(
                        x =>
                            x.Code == ParseResultDictionary.VALID_10_PHONE_CODE ||
                            x.Code == ParseResultDictionary.VALID_7_PHONE_CODE));
            }
        }

        public string SuggestedAddress()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", NameFull, AddressLine1, AddressLine2, City, State,
                PostalCode);
        }

        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        public string Error
        {
            get { return Errors.ToString(); }
        }

        public string Warning
        {
            get { return Warnings.ToString(); }
        }

        public string ParseResults
        {
            get { return Results.ToString(); }
        }

        public List<ParseResult> AutoFixes
        {
            get { return Results.Where(x => x.Type == ParseResult.AUTOFIX).ToList(); }
        }

        public List<ParseResult> Errors
        {
            get { return Results.Where(x => x.Type == ParseResult.ERROR).ToList(); }
        }

        public List<ParseResult> Warnings
        {
            get { return Results.Where(x => x.Type == ParseResult.WARN).ToList(); }
        }

        public List<ParseResult> Informational
        {
            get { return Results.Where(x => x.Type == ParseResult.INFO).ToList(); }
        }

        public bool HasAutoFixes => AutoFixes.Count > 0;

        public bool HasNewStreetAddressLine1
        {
            get
            {
                var v = (Results.Select(x => x.Code).Intersect(ParseResultDictionary.AUTOFIX_STREET_ADDRESS_CODES));
                return !v.Any();
            }
        }
        public bool HasNewCity
        {
            get { return (Results.Any(x => x.Code == ParseResultDictionary.AUTOFIX_CITY_CODE)); }
        }
    }
}
