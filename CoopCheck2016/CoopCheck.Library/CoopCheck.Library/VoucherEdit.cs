using Csla;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using DataClean;

namespace CoopCheck.Library
{
    public partial class VoucherEdit
    {
        public string FullName
        {
            get
            {
                var retVal = string.Empty;
                retVal = AddSpace(this.NamePrefix) + AddSpace(this.First) + AddSpace(this.Middle) + AddSpace(this.Last) + AddSpace(this.Suffix);
                return retVal.TrimEnd(' ');
            }
        }
        private string AddSpace(string value)
        {
            var retVal = string.Empty;
            if (value.Length > 0)
            {
                retVal = value + " ";
            }
            return retVal;
        }
        #region OnDeserialized actions

        /*/// <summary>
        /// This method is called on a newly deserialized object
        /// after deserialization is complete.
        /// </summary>
        /// <param name="context">Serialization context object.</param>
        protected override void OnDeserialized(System.Runtime.Serialization.StreamingContext context)
        {
            base.OnDeserialized(context);
            // add your custom OnDeserialized actions here.
        }*/

        #endregion

        #region Pseudo Event Handlers

        //partial void OnCreate(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

#if !SILVERLIGHT

        //partial void OnDeletePre(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        //partial void OnDeletePost(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        //partial void OnFetchPre(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        //partial void OnFetchPost(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        //partial void OnFetchRead(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        //partial void OnUpdatePre(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        //partial void OnUpdatePost(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        //partial void OnInsertPre(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        //partial void OnInsertPost(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

#endif

        #endregion
        #region Business Rules
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            // TODO: add business rules
            var props = FieldManager.GetRegisteredProperties();
            BusinessRules.AddRule(new ValidAddressRule(props));
           
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }


        public class ValidAddressRule : Csla.Rules.ObjectRule
        {

            public ValidAddressRule(IEnumerable<Csla.Core.IPropertyInfo> fields)
            {
                AffectedProperties.AddRange(fields);
            }

            protected override void Execute(Csla.Rules.RuleContext context)
            {
                var target = (VoucherEdit)context.Target;
                string firstName = (string)ReadProperty(target, FirstProperty);
                string lastName = (string)ReadProperty(target, LastProperty);
                string companyName = (string)ReadProperty(target, CompanyProperty);


                if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(companyName))
                {
                    context.AddErrorResult(FirstProperty, "First Name and Last Name Or Company Name Required");
                    context.AddErrorResult(LastProperty, "First Name and Last Name Or Company Name Required");
                    context.AddErrorResult(CompanyProperty, "First Name and Last Name Or Company Name Required");
                }
                else // if the voucher is otherwise valid check Melissa Data Service
                {
                    var config = ConfigurationManager.AppSettings;
                    var chk = new DataCleaner(config);
                    var inputAddress = new InputStreetAddress()
                    {
                        AddressLine1 = target.AddressLine1,
                        AddressLine2 = target.AddressLine2,
                        City = target.Municipality,
                        CompanyName = target.Company,
                        Country = target.Country,
                        PostalCode = target.PostalCode,
                        FullName = target.FullName,
                        State = target.Region
                    };

                    OutputStreetAddress outputAddress;

                    var isValid = chk.VerifyAndCleanAddress(inputAddress, out outputAddress);

                    if (!isValid)
                    {
                        foreach (var e in outputAddress.Errors)
                        {
                            context.AddErrorResult(e.LongDescription);
                        }
                    }
                 
                }
            }
        }
        
        

        #endregion
        public static VoucherEdit NewVoucherEdit(decimal amount, string personId, string namePrefix, string firstName, string middleName, string lastName, string suffix, string title, 
                string company, string address1, string address2, string municipality, string region, string postalCode, string country, string phone, string email)
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
            if (country!=string.Empty)
            { 
                voc.Country = country;
            }
            voc.PhoneNumber = phone;
            voc.EmailAddress = email;

            return voc;
        }
    }
}
