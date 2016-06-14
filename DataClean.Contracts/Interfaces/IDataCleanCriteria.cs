namespace DataClean.Contracts.Interfaces
{
    public interface IDataCleanCriteria
    {
        bool AutoFixPostalCode { get; set; }
        bool AutoFixState { get; set; }
        bool AutoFixAddressLine1 { get; set; }
        bool AutoFixCity { get; set; }
        bool ForceValidation { get; set; }
        string ToFormattedString(char token);
    }
}