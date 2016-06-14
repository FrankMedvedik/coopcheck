using CoopCheck.WPF.Contracts.Interfaces;

namespace CoopCheck.WPF.Models
{
    public class ColumnPropertyMap : IColumnPropertyMap
    {
        public string ColumnName { get; set; }
        public bool Available { get; set; }
        public int ColumnOffset { get; set; }
    }
}