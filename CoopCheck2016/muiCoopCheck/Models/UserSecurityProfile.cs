
using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;

namespace CoopCheck.WPF.Models
{
    public class UserSecurityProfile
    {
        private string _errorMessage;
        private string _statusMessage;
        private bool _isBusy = false; // set to false by default to cancel previous busy...

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; }
        }



        public bool CanEditVouchers
        {
            get { return true; }
        }

        public bool CanReadVouchers
        {
            get { return true; }
        }

        public bool CanReadPayments
        {
            get { return true; }
        }

        public bool CanMakePayments
        {
            get { return true; }
        }

        public string whoami
        {
            get { return Environment.UserName; }
        }
        public List<PropertyValueCollection>  Groups(string userDn, bool recursive)
        {
            var groupMemberships = new List<PropertyValueCollection>();
            return AttributeValuesMultiString("memberOf", userDn,
                groupMemberships, recursive);
        }


        public List<PropertyValueCollection>  AttributeValuesMultiString(string attributeName, string objectDn, List<PropertyValueCollection>  valuesCollection, bool recursive)
        {
            DirectoryEntry ent = new DirectoryEntry(objectDn);
            var valueCollection = ent.Properties[attributeName];
            IEnumerator en = valueCollection.GetEnumerator();

            //while (en.MoveNext())
            //{
            //    if (en.Current != null)
            //    {
            //        if (!valuesCollection.Contains(en.Current))
            //        {
            //            valuesCollection.Add(en.Current);
            //            if (recursive)
            //            {
            //                AttributeValuesMultiString(attributeName, "LDAP://" +
            //                en.Current.ToString(), valuesCollection, true);
            //            }
            //        }
            //    }
            //}
            ent.Close();
            ent.Dispose();
            return valuesCollection;
        }
    }
}
