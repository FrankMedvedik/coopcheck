using System.Collections.Generic;
using DataClean.Contracts.Models;

namespace DataClean.Contracts.Interfaces
{
    public  interface IAutoFixer
    {
        List<DataCleanEvent> DataCleanEvents { get; set; }
        void ApplyFixes();
    }
}