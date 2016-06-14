namespace CoopCheck.Repository.Contracts.Interfaces
{
    public interface ICoopCheckClient
    {
        string Address { get; set; }
        string Attention { get; set; }
        string City { get; set; }
        string ClientID { get; set; }
        string ClientName { get; set; }
        string Country { get; set; }
        string Email { get; set; }
        string Fax { get; set; }
        bool? inSolomon { get; set; }
        string Phone { get; set; }
        string State { get; set; }
        string Zip { get; set; }
    }
}