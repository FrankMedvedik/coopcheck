using System.Collections.Generic;
using DataClean.Models;

namespace DataClean.Interfaces
{
    public interface IDataCleanRepository
    {
        DataCleanEvent GetEvent(int id);
        void SaveEvent(DataCleanEvent e);
        List<ParseResult> GetMelissaReference();
    }
}