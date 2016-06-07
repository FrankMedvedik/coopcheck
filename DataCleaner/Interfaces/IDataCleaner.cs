using System;
using System.Collections.Generic;
using DataClean.Models;

namespace DataClean.Interfaces
{
    public interface IDataCleaner
    {
        Boolean VerifyAndCleanAddress(InputStreetAddress inA, out OutputStreetAddress outA);
        OutputStreetAddress[] VerifyAndCleanAddress(InputStreetAddress[] inputAddressArray);
    }
}