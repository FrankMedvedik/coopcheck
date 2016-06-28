using System.Collections.Generic;
using DataClean.Contracts.Models;
using DataClean.Repository.Contracts.Interfaces;

namespace DataClean.Contracts.Interfaces
{
    public interface IDataCleanRepository
    {
        DataCleanEvent GetEvent(int id);
        void SaveEvent(DataCleanEvent e);
        List<ParseResult> GetMelissaReference();
        void SaveStats(IDataCleanStat d);
    }
}