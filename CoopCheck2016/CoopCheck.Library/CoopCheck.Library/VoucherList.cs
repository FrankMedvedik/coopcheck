using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;

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
        
        
        #region Authorization
#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
#else
        protected static void AddObjectAuthorizationRules()
#endif
        {
            //BusinessRules.AddRule(typeof(Account), new IsInRole(AuthorizationActions.CreateObject, "RECKNER\\CoopCheckAdmin"));
            //BusinessRules.AddRule(typeof(Account), new IsInRole(AuthorizationActions.GetObject, "RECKNER\\CoopCheckReader"));
            //BusinessRules.AddRule(typeof(Account), new IsInRole(AuthorizationActions.EditObject, "RECKNER\\CoopCheckAdmin"));
            //BusinessRules.AddRule(typeof(Account), new IsInRole(AuthorizationActions.DeleteObject, "RECKNER\\CoopCheckAdmin"));

            //AddObjectAuthorizationRulesExtend();
        }

        /// <summary>
        /// Allows the set up of custom object authorization rules.
        /// </summary>
        static partial void AddObjectAuthorizationRulesExtend();

        /// <summary>
        /// Checks if the current user can create a new Account object.
        /// </summary>
        /// <returns><c>true</c> if the user can create a new object; otherwise, <c>false</c>.</returns>
        public static bool CanAddObject()
        {
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.CreateObject, typeof(Account));
        }

        /// <summary>
        /// Checks if the current user can retrieve Account's properties.
        /// </summary>
        /// <returns><c>true</c> if the user can read the object; otherwise, <c>false</c>.</returns>
        public static bool CanGetObject()
        {
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.GetObject, typeof(Account));
        }

        /// <summary>
        /// Checks if the current user can change Account's properties.
        /// </summary>
        /// <returns><c>true</c> if the user can update the object; otherwise, <c>false</c>.</returns>
        public static bool CanEditObject()
        {
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.EditObject, typeof(Account));
        }

        /// <summary>
        /// Checks if the current user can delete a Account object.
        /// </summary>
        /// <returns><c>true</c> if the user can delete the object; otherwise, <c>false</c>.</returns>
        public static bool CanDeleteObject()
        {
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.DeleteObject, typeof(Account));
        }

        #endregion
    }
}
