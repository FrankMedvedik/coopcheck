using System.Collections.Generic;

namespace DataClean.Contracts.Interfaces
{
    public  interface IAutoFixer
    {
        List<IDataCleanEvent> DataCleanEvents { get; set; }
        void ApplyFixes();
    }
}