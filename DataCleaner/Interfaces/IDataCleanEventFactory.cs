using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClean.Interfaces
{
    interface IDataCleanEventFactory
    {
        IDataCleanEvent GetNew(IInputStreetAddress input);
        IEnumerable<IDataCleanEvent> GetNew(IEnumerable<IInputStreetAddress> input);

    }
}
