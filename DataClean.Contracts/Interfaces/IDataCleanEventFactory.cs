using System.Collections.Generic;

namespace DataClean.Contracts.Interfaces
{

        public interface IDataCleanEventFactory
        {
            IDataCleanEvent ValidateAddress(IInputStreetAddress input);
            IDataCleanEvent GetExistingEvent(int id);
            List<IDataCleanEvent> ValidateAddresses(List<IInputStreetAddress> inputAddressList);
        }
    }