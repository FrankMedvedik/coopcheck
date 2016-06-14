using System;

namespace DataClean.Contracts.Interfaces
{
    public interface IDataCleaner
    {
            bool ForceValidation { get; set; }
            Boolean VerifyAndCleanAddress(IInputStreetAddress inA, out IOutputStreetAddress outA);
            IOutputStreetAddress[] VerifyAndCleanAddress(IInputStreetAddress[] inputAddressArray);
        }
    }
