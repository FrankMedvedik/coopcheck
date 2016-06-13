using System;
using System.Collections.Generic;
using System.ComponentModel;
using DataClean.Models;

namespace DataClean.Interfaces
{
    public interface IOutputStreetAddress : IDataErrorInfo
    {
        string AddressDeliveryInstallation { get; set; }
        string AddressExtras { get; set; }
        string AddressHouseNumber { get; set; }
        string AddressKey { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string AddressLockBox { get; set; }
        string AddressPostDirection { get; set; }
        string AddressPreDirection { get; set; }
        string AddressPrivateMailboxName { get; set; }
        string AddressPrivateMailboxRange { get; set; }
        string AddressRouteService { get; set; }
        string AddressStreetName { get; set; }
        string AddressStreetSuffix { get; set; }
        string AddressSuiteName { get; set; }
        string AddressSuiteNumber { get; set; }
        string AddressTypeCode { get; set; }
        string AreaCode { get; set; }
        string CBSACode { get; set; }
        string CBSADivisionCode { get; set; }
        string CBSADivisionLevel { get; set; }
        string CBSADivisionTitle { get; set; }
        string CBSALevel { get; set; }
        string CBSATitle { get; set; }
        string CarrierRoute { get; set; }
        string CensusBlock { get; set; }
        string CensusTract { get; set; }
        string City { get; set; }
        string CityAbbreviation { get; set; }
        string CompanyName { get; set; }
        string CongressionalDistrict { get; set; }
        string CountryCode { get; set; }
        string CountryName { get; set; }
        string CountyFIPS { get; set; }
        string CountyName { get; set; }
        string DateOfBirth { get; set; }
        string DateOfDeath { get; set; }
        string DeliveryIndicator { get; set; }
        string DeliveryPointCheckDigit { get; set; }
        string DeliveryPointCode { get; set; }
        string DemographicsGender { get; set; }
        string DemographicsResults { get; set; }
        string DomainName { get; set; }
        string EmailAddress { get; set; }
        string Gender { get; set; }
        string Gender2 { get; set; }
        string HouseholdIncome { get; set; }
        int ID { get; set; }
        string Latitude { get; set; }
        string LengthOfResidence { get; set; }
        string Longitude { get; set; }
        string MailboxName { get; set; }
        string MaritalStatus { get; set; }
        string NameFirst { get; set; }
        string NameFirst2 { get; set; }
        string NameFull { get; set; }
        string NameLast { get; set; }
        string NameLast2 { get; set; }
        string NameMiddle { get; set; }
        string NameMiddle2 { get; set; }
        string NamePrefix { get; set; }
        string NamePrefix2 { get; set; }
        string NameSuffix { get; set; }
        string NameSuffix2 { get; set; }
        string NewAreaCode { get; set; }
        string Occupation { get; set; }
        string OwnRent { get; set; }
        string PhoneExtension { get; set; }
        string PhoneNumber { get; set; }
        string PhonePrefix { get; set; }
        string PhoneSuffix { get; set; }
        string PlaceCode { get; set; }
        string PlaceName { get; set; }
        string Plus4 { get; set; }
        string PostalCode { get; set; }
        string PresenceOfChildren { get; set; }
        string PrivateMailBox { get; set; }
        string RecordExtras { get; set; }
        List<IParseResult> Results { get; set; }
        List<IParseResult> Errors { get; }
        List<IParseResult> Warnings { get; }
        List<IParseResult> Informational { get; }
        string Salutation { get; set; }
        string State { get; set; }
        string StateName { get; set; }
        string Suite { get; set; }
        string TopLevelDomain { get; set; }
        string UTC { get; set; }
        string UrbanizationName { get; set; }
        Boolean OkPhone { get; }
        Boolean OkEmailAddress { get; }
        Boolean OkMailingAddress { get; }

        string ToString();
    }
}