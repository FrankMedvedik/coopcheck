using System.Collections.Generic;
using DataClean.Models;

namespace DataClean.Interfaces
{
    public interface IDataCleanRepository
    {
        IDataCleanEvent GetEvent(int id);
        void SaveEvent(IDataCleanEvent e);
        List<IParseResult> GetMelissaReference();
    }
}