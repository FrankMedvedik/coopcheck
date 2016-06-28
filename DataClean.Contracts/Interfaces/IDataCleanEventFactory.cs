using System.Collections.Generic;
using DataClean.Contracts.Models;

namespace DataClean.Contracts.Interfaces
{

        public interface IDataCleanEventFactory
        {
            DataCleanEvent ValidateAddress(InputStreetAddress input);
            DataCleanEvent GetExistingEvent(int id);
            List<DataCleanEvent> ValidateAddresses(List<InputStreetAddress> inputAddressList);
        }
    }