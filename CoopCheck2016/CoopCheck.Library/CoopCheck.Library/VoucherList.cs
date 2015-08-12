
namespace CoopCheck.Library
{
    public partial class VoucherList
    {

        #region OnDeserialized actions

        /*/// <summary>
        /// This method is called on a newly deserialized object
        /// after deserialization is complete.
        /// </summary>
        protected override void OnDeserialized()
        {
            base.OnDeserialized();
            // add your custom OnDeserialized actions here.
        }*/

        #endregion

        #region Pseudo Event Handlers

#if !SILVERLIGHT

        //partial void OnFetchPre(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        //partial void OnFetchPost(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

#endif

        #endregion
        public decimal TotalAmount()
        {
            decimal retVal = 0;
            foreach (var voc in this)
            {
                retVal = retVal + voc.Amount ?? 0;
            }
            return retVal;
        }
        public void AddVoucher(decimal amount, string personId, string namePrefix, string firstName, string middleName, string lastName, string suffix, string title, string company, string address1, string address2,
               string municipality, string region, string postalCode, string country, string phone, string email)
        {
            var voc = VoucherEdit.NewVoucherEdit();
            voc.Amount = amount;
            voc.PersonId = personId;
            voc.NamePrefix = namePrefix;
            voc.First = firstName;
            voc.Middle = middleName;
            voc.Last = lastName;
            voc.Suffix = suffix;
            voc.Title = title;
            voc.Company = company;
            voc.AddressLine1 = address1;
            voc.AddressLine2 = address2;
            voc.Municipality = municipality;
            voc.Region = region;
            voc.PostalCode = postalCode;
            voc.Country = country;
            voc.PhoneNumber = phone;
            voc.EmailAddress = email;

            Add(voc);
        }
        public VoucherEdit Find(int Id)
        {
            var retVal = VoucherEdit.NewVoucherEdit();
            foreach (VoucherEdit voc in this)
            {
                if (voc.Id == Id)
                {
                    retVal = voc;
                    break;
                }
            }
            return retVal;
        }
    }
}
