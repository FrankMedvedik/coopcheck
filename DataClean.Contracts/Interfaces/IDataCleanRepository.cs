using System.Collections.Generic;
using DataClean.Repository.Contracts.Interfaces;

namespace DataClean.Contracts.Interfaces
{
    public interface IDataCleanRepository
    {
        IDataCleanEvent GetEvent(int id);
        void SaveEvent(IDataCleanEvent e);
        List<IParseResult> GetMelissaReference();
        void SaveStats(IDataCleanStat d);
    }
}