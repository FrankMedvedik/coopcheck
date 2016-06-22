using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClean.Contracts.Models;
using DataClean.Personator;

namespace DataClean.Converters
{
    public class ResponseToOutputStreetAddressConverter
    {
        public static OutputStreetAddress ProcessResponseRecord(ResponseRecord respRec, ParseResultDictionary _msgDict )
        {

            var o = new OutputStreetAddress()
            {
                AddressDeliveryInstallation = respRec.AddressDeliveryInstallation,
                AddressExtras = respRec.AddressExtras,
                AddressHouseNumber = respRec.AddressHouseNumber,
                AddressKey = respRec.AddressKey,
                AddressLine1 = respRec.AddressLine1,
                AddressLine2 = respRec.AddressLine2,
                AddressLockBox = respRec.AddressLockBox,
                AddressPostDirection = respRec.AddressPostDirection,
                AddressPreDirection = respRec.AddressPreDirection,
                AddressPrivateMailboxName = respRec.AddressPrivateMailboxName,
                AddressPrivateMailboxRange = respRec.AddressPrivateMailboxRange,
                AddressRouteService = respRec.AddressRouteService,
                AddressStreetName = respRec.AddressStreetName,
                AddressStreetSuffix = respRec.AddressStreetSuffix,
                AddressSuiteName = respRec.AddressSuiteName,
                AddressSuiteNumber = respRec.AddressSuiteNumber,
                AddressTypeCode = respRec.AddressTypeCode,
                AreaCode = respRec.AreaCode,
                CBSACode = respRec.CBSACode,
                CBSADivisionCode = respRec.CBSADivisionCode,
                CBSADivisionLevel = respRec.CBSADivisionLevel,
                CBSADivisionTitle = respRec.CBSADivisionTitle,
                CBSALevel = respRec.CBSALevel,
                CBSATitle = respRec.CBSATitle,
                CarrierRoute = respRec.CarrierRoute,
                CensusBlock = respRec.CensusBlock,
                CensusTract = respRec.CensusTract,
                City = respRec.City,
                CityAbbreviation = respRec.CityAbbreviation,
                CompanyName = respRec.CompanyName,
                CongressionalDistrict = respRec.CongressionalDistrict,
                CountryCode = respRec.CountryCode,
                CountryName = respRec.CountryName,
                CountyFIPS = respRec.CountyFIPS,
                CountyName = respRec.CountyName,
                DateOfBirth = respRec.DateOfBirth,
                DateOfDeath = respRec.DateOfDeath,
                DeliveryIndicator = respRec.DeliveryIndicator,
                DeliveryPointCheckDigit = respRec.DeliveryPointCheckDigit,
                DeliveryPointCode = respRec.DeliveryPointCode,
                DemographicsGender = respRec.DemographicsGender,
                DemographicsResults = respRec.DemographicsResults,
                DomainName = respRec.DomainName,
                EmailAddress = respRec.EmailAddress,
                Gender = respRec.Gender,
                Gender2 = respRec.Gender2,
                HouseholdIncome = respRec.HouseholdIncome,
                Latitude = respRec.Latitude,
                LengthOfResidence = respRec.LengthOfResidence,
                Longitude = respRec.Longitude,
                MailboxName = respRec.MailboxName,
                MaritalStatus = respRec.MaritalStatus,
                NameFirst = respRec.NameFirst,
                NameFirst2 = respRec.NameFirst2,
                NameFull = respRec.NameFull,
                NameLast = respRec.NameLast,
                NameLast2 = respRec.NameLast2,
                NameMiddle = respRec.NameMiddle,
                NameMiddle2 = respRec.NameMiddle2,
                NamePrefix = respRec.NamePrefix,
                NamePrefix2 = respRec.NamePrefix2,
                NameSuffix = respRec.NameSuffix,
                NameSuffix2 = respRec.NameSuffix2,
                NewAreaCode = respRec.NewAreaCode,
                Occupation = respRec.Occupation,
                OwnRent = respRec.OwnRent,
                PhoneExtension = respRec.PhoneExtension,
                PhoneNumber = respRec.PhoneNumber,
                PhonePrefix = respRec.PhonePrefix,
                PhoneSuffix = respRec.PhoneSuffix,
                PlaceCode = respRec.PlaceCode,
                PlaceName = respRec.PlaceName,
                Plus4 = respRec.Plus4,
                PostalCode = respRec.PostalCode,
                PresenceOfChildren = respRec.PresenceOfChildren,
                PrivateMailBox = respRec.PrivateMailBox,
                RecordExtras = respRec.RecordExtras,
                Results = _msgDict.LookupCodeList(respRec.Results.Split(',')).ToList(),
                Salutation = respRec.Salutation,
                State = respRec.State,
                StateName = respRec.StateName,
                Suite = respRec.Suite,
                TopLevelDomain = respRec.TopLevelDomain,
                UTC = respRec.UTC,
                UrbanizationName = respRec.UrbanizationName
            };

            var i = SetIDS(respRec.RecordID);
            o.RecordID = i[0];
            o.ID = (int) int.Parse(i[1]);
            return o;
        }
    private static string[] SetIDS(string recordId)
    {
        return recordId.Split('|');
    }

    private static bool SetAddressOk(OutputStreetAddress outputStreetAddress)
        {
         if(outputStreetAddress.Errors.Count() != 0) return false;
            return true;
        }
    }
}
