using System;
using System.Collections.Generic;
using DataClean.Contracts.Models;

namespace DataClean.Contracts.Interfaces
{
    public interface IDataCleaner
    {
            bool ForceValidation { get; set; }
            Boolean VerifyAndCleanAddress(InputStreetAddress inA, out OutputStreetAddress outA);
            IEnumerable<OutputStreetAddress> VerifyAndCleanAddress(InputStreetAddress[] inputAddressArray);
        }
    }
