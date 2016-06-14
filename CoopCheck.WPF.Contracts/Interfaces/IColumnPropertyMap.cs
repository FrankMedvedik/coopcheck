namespace CoopCheck.WPF.Contracts.Interfaces
{
    public interface IColumnPropertyMap
    {
        string ColumnName { get; set; }
        bool Available { get; set; }
        int ColumnOffset { get; set; }
    }
}