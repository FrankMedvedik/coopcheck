﻿using System.Collections.Generic;
using DataClean.Contracts.Interfaces;
using DataClean.Models;
using Newtonsoft.Json;

namespace DataClean.DataCleaner
{
    class AutoFixer : IAutoFixer
    {
        public static IParseResult UpdatedAddressLine1ParseResult = new ParseResult() { AlternateAddressExists = true, Code = ParseResultDictionary.AUTOFIX_STREET_ADDRESS_CODE, LongDescription = "Address Line 1 replaced: Old Value = ", ShortDescription = "Address 1 replaced", Type = ParseResult.AUTOFIX };
        public static IParseResult UpdatedCityParseResult = new ParseResult() { AlternateAddressExists = true, Code = ParseResultDictionary.AUTOFIX_CITY_CODE, LongDescription = "City replaced: Old Value = ", ShortDescription = "City replaced", Type = ParseResult.AUTOFIX };
        public static IParseResult UpdatedStateParseResult = new ParseResult() { AlternateAddressExists = true, Code = ParseResultDictionary.AUTOFIX_STATE_CODE, LongDescription = "State replaced: Old Value =  ", ShortDescription = "state replaced", Type = ParseResult.AUTOFIX };
        public static IParseResult UpdatedPostalCodeParseResult = new ParseResult() { AlternateAddressExists = true, Code = ParseResultDictionary.AUTOFIX_POSTAL_CODE, LongDescription = "Postal Code replaced: Old Value = ", ShortDescription = "Postal code replaced", Type = ParseResult.AUTOFIX };

        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
        public AutoFixer(IDataCleanCriteria criteria)
        {
            _criteria = criteria;
        }

        private IDataCleanCriteria _criteria;
        public List<IDataCleanEvent> DataCleanEvents { get; set; }

        private void FixDataCleanEvent(int idx)
        {
            if (_criteria.AutoFixAddressLine1)
                AutoFixAddressLine1(idx);

            if (_criteria.AutoFixPostalCode)
                AutoFixPostalCode(idx);

            if (_criteria.AutoFixState)
                AutoFixState(idx);

            if (_criteria.AutoFixCity)
                AutoFixCity(idx);

        }
        public void ApplyFixes()
        {
            for (int index = 0; index < DataCleanEvents.Count; index++)
            {
                FixDataCleanEvent(index);
            }
        }

        private void AutoFixAddressLine1(int i)
        {
            if (DataCleanEvents[i].Output.AddressLine1 != null)
            {
                DataCleanEvents[i].Output.Results.Add(AddOldValue(UpdatedAddressLine1ParseResult, DataCleanEvents[i].Input.AddressLine1));
                DataCleanEvents[i].Input.AddressLine1 = DataCleanEvents[i].Output.AddressLine1;
            }
        }

        private IParseResult AddOldValue(IParseResult parseResultTemplate, string oldValue)
        {
            var workParseResult = Clone<IParseResult>(parseResultTemplate);
            workParseResult.LongDescription = parseResultTemplate.LongDescription + oldValue;
            return workParseResult;
        }

        private void AutoFixCity(int i)
        {
            if (DataCleanEvents[i].Output.City != null)
            {
                DataCleanEvents[i].Output.Results.Add(AddOldValue(UpdatedCityParseResult, DataCleanEvents[i].Input.City));
                DataCleanEvents[i].Input.City = DataCleanEvents[i].Output.City;
            }

        }

        private void AutoFixState(int i)
        {
            if (DataCleanEvents[i].Output.State != null)
            {
                DataCleanEvents[i].Output.Results.Add(AddOldValue(UpdatedStateParseResult, DataCleanEvents[i].Input.State));
                DataCleanEvents[i].Input.State = DataCleanEvents[i].Output.State;
            }

        }

        private void AutoFixPostalCode(int i)
        {
            if (DataCleanEvents[i].Output.PostalCode != null && DataCleanEvents[i].Input.PostalCode != DataCleanEvents[i].Output.PostalCode)
            {
                DataCleanEvents[i].Output.Results.Add(AddOldValue(UpdatedPostalCodeParseResult, DataCleanEvents[i].Input.PostalCode));
                DataCleanEvents[i].Input.PostalCode = DataCleanEvents[i].Output.PostalCode;
            }

        }

    }
}