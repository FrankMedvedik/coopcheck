namespace CoopCheck.Library.Interfaces
{
    public interface IVoucherList
    {
        void AddVoucher(decimal amount, string personId, string namePrefix, string firstName, string middleName, string lastName, string suffix, string title, string company, string address1, string address2, string municipality, string region, string postalCode, string country, string phone, string email);
        bool Contains(int id);
        bool ContainsDeleted(int id);
        IVoucherEdit Find(int Id);
        void Remove(int id);
        decimal TotalAmount();
    }
}