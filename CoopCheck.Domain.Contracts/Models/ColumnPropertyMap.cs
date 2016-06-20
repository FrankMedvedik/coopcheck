using CoopCheck.Domain.Contracts.Interfaces;

namespace CoopCheck.Domain.Contracts.Models
{
    public class ColumnPropertyMap : IColumnPropertyMap
    {
        public string ColumnName { get; set; }
        public bool Available { get; set; }
        public int ColumnOffset { get; set; }
    }
}