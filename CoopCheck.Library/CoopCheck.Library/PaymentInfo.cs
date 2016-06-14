using System;
using CoopCheck.Library.Contracts.Interfaces;
using Csla;
#if SILVERLIGHT
using Csla.Serialization;
#else
using CoopCheck.DAL;
#endif

namespace CoopCheck.Library
{
    [Serializable]
    public class PaymentInfo : ReadOnlyBase<PaymentInfo>, IPaymentInfo
    {
#region "Properties

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }  
        public static readonly PropertyInfo<int> BatchNumProperty = RegisterProperty<int>(c => c.BatchNum);
        public int BatchNum
        {
            get { return GetProperty(BatchNumProperty); }
            private set { LoadProperty(BatchNumProperty, value); }
        }
        public static readonly PropertyInfo<SmartDate> PayDateProperty = RegisterProperty<SmartDate>(c => c.PayDate);
        public SmartDate PayDate
        {
            get { return GetProperty(PayDateProperty); }
            private set { LoadProperty(PayDateProperty, value); }
        }
        public static readonly PropertyInfo<int> JobNumProperty = RegisterProperty<int>(c => c.JobNum);
        public int JobNum
        {
            get { return GetProperty(JobNumProperty); }
            private set { LoadProperty(JobNumProperty, value); }
        }
        public static readonly PropertyInfo<string> ThankYou1Property = RegisterProperty<string>(c => c.ThankYou1);
        public string ThankYou1
        {
            get { return GetProperty(ThankYou1Property); }
            private set { LoadProperty(ThankYou1Property, value); }
        }
        public static readonly PropertyInfo<string> StudyTopicProperty = RegisterProperty<string>(c => c.StudyTopic);
        public string StudyTopic
        {
            get { return GetProperty(StudyTopicProperty); }
            private set { LoadProperty(StudyTopicProperty, value); }
        }
        public static readonly PropertyInfo<string> ThankYou2Property = RegisterProperty<string>(c => c.ThankYou2);
        public string ThankYou2
        {
            get { return GetProperty(ThankYou2Property); }
            private set { LoadProperty(ThankYou2Property, value); }
        }
        public static readonly PropertyInfo<string> MarketingResearchMessageProperty = RegisterProperty<string>(c => c.MarketingResearchMessage);
        public string MarketingResearchMessage
        {
            get { return GetProperty(MarketingResearchMessageProperty); }
            private set { LoadProperty(MarketingResearchMessageProperty, value); }
        }
        public static readonly PropertyInfo<SmartDate> CheckDateProperty = RegisterProperty<SmartDate>(c => c.CheckDate);
        public SmartDate CheckDate
        {
            get { return GetProperty(CheckDateProperty); }
            private set { LoadProperty(CheckDateProperty, value); }
        }
        public static readonly PropertyInfo<string> CheckNumProperty = RegisterProperty<string>(c => c.CheckNum);
        public string CheckNum
        {
            get { return GetProperty(CheckNumProperty); }
            private set { LoadProperty(CheckNumProperty, value); }
        }
        public static readonly PropertyInfo<decimal> AmountProperty = RegisterProperty<decimal>(c => c.Amount);
        public decimal Amount
        {
            get { return GetProperty(AmountProperty); }
            private set { LoadProperty(AmountProperty, value); }
        }
        public static readonly PropertyInfo<string> PersonIdProperty = RegisterProperty<string>(c => c.PersonId);
        public string PersonId
        {
            get { return GetProperty(PersonIdProperty); }
            private set { LoadProperty(PersonIdProperty, value); }
        }
        public static readonly PropertyInfo<string> PrefixProperty = RegisterProperty<string>(c => c.Prefix);
        public string Prefix
        {
            get { return GetProperty(PrefixProperty); }
            private set { LoadProperty(PrefixProperty, value); }
        }
        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(c => c.FirstName);
        public string FirstName
        {
            get { return GetProperty(FirstNameProperty); }
            private set { LoadProperty(FirstNameProperty, value); }
        }
        public static readonly PropertyInfo<string> MiddleNameProperty = RegisterProperty<string>(c => c.MiddleName);
        public string MiddleName
        {
            get { return GetProperty(MiddleNameProperty); }
            private set { LoadProperty(MiddleNameProperty, value); }
        }
        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);
        public string LastName
        {
            get { return GetProperty(LastNameProperty); }
            private set { LoadProperty(LastNameProperty, value); }
        }
        public static readonly PropertyInfo<string> NameSuffixProperty = RegisterProperty<string>(c => c.NameSuffix);
        public string NameSuffix
        {
            get { return GetProperty(NameSuffixProperty); }
            private set { LoadProperty(NameSuffixProperty, value); }
        }
        public string FullName
        {
            get
            {
                var retVal = string.Empty;

                retVal = Util.AddSpace(GetProperty(PrefixProperty)) +
                         Util.AddSpace(GetProperty(FirstNameProperty)) +
                         Util.AddSpace(GetProperty(MiddleNameProperty)) +
                         Util.AddSpace(GetProperty(LastNameProperty)) +
                         Util.AddSpace(GetProperty(NameSuffixProperty));

                return retVal;
            }
        }
        public static readonly PropertyInfo<string> TitleProperty = RegisterProperty<string>(c => c.Title);
        public string Title
        {
            get { return GetProperty(TitleProperty); }
            private set { LoadProperty(TitleProperty, value); }
        }
        public static readonly PropertyInfo<string> CompanyProperty = RegisterProperty<string>(c => c.Company);
        public string Company
        {
            get { return GetProperty(CompanyProperty); }
            private set { LoadProperty(CompanyProperty, value); }
        }
        public static readonly PropertyInfo<string> Address1Property = RegisterProperty<string>(c => c.Address1);
        public string Address1
        {
            get { return GetProperty(Address1Property); }
            private set { LoadProperty(Address1Property, value); }
        }
        public static readonly PropertyInfo<string> Address2Property = RegisterProperty<string>(c => c.Address2);
        public string Address2
        {
            get { return GetProperty(Address2Property); }
            private set { LoadProperty(Address2Property, value); }
        }
        public static readonly PropertyInfo<string> MunicipalityProperty = RegisterProperty<string>(c => c.Municipality);
        public string Municipality
        {
            get { return GetProperty(MunicipalityProperty); }
            private set { LoadProperty(MunicipalityProperty, value); }
        }
        public static readonly PropertyInfo<string> RegionProperty = RegisterProperty<string>(c => c.Region);
        public string Region
        {
            get { return GetProperty(RegionProperty); }
            private set { LoadProperty(RegionProperty, value); }
        }
        public static readonly PropertyInfo<string> PostalCodeProperty = RegisterProperty<string>(c => c.PostalCode);
        public string PostalCode
        {
            get { return GetProperty(PostalCodeProperty); }
            private set { LoadProperty(PostalCodeProperty, value); }
        }
        public static readonly PropertyInfo<string> CountryProperty = RegisterProperty<string>(c => c.Country);
        public string Country
        {
            get { return GetProperty(CountryProperty); }
            private set { LoadProperty(CountryProperty, value); }
        }
        public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(c => c.Email);
        public string Email
        {
            get { return GetProperty(EmailProperty); }
            private set { LoadProperty(EmailProperty, value); }
        }
        public static readonly PropertyInfo<string> PhoneNumberProperty = RegisterProperty<string>(c => c.PhoneNumber);
        public string PhoneNumber
        {
            get { return GetProperty(PhoneNumberProperty); }
            private set { LoadProperty(PhoneNumberProperty, value); }
        }
        public static readonly PropertyInfo<bool> CompletedProperty = RegisterProperty<bool>(c => c.Completed);
        public bool Completed
        {
            get { return GetProperty(CompletedProperty); }
            private set { LoadProperty(CompletedProperty, value); }
        }
#endregion
        #region Factory Methods
#if !SILVERLIGHT
        internal static PaymentInfo GetPaymentInfo(PaymentInfoDto data){
            PaymentInfo o = new PaymentInfo();
            o.Fetch(data);
            return o;
        }
#endif
        #endregion
        #region Constructor
#if SILVERLIGHT
         [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public PaymentInfo()
#else
        private PaymentInfo()
#endif
        {

        }
#endregion
#region Data Access
#if !SILVERLIGHT
        private void Fetch(PaymentInfoDto data){
            LoadProperty(IdProperty, data.Id);
            LoadProperty(BatchNumProperty, data.BatchNum);
            LoadProperty(PayDateProperty, data.PayDate);
            LoadProperty(JobNumProperty, data.JobNum);
            LoadProperty(ThankYou1Property, data.ThankYou1);
            LoadProperty(StudyTopicProperty, data.StudyTopic);
            LoadProperty(ThankYou2Property, data.ThankYou2);
            LoadProperty(MarketingResearchMessageProperty, data.MarketingResearchMessage);
            LoadProperty(CheckDateProperty, data.CheckDate);
            LoadProperty(CheckNumProperty, data.CheckNum);
            LoadProperty(AmountProperty, data.Amount);
            LoadProperty(PersonIdProperty, data.PersonId);
            LoadProperty(PrefixProperty, data.Prefix);
            LoadProperty(FirstNameProperty, data.FirstName);
            LoadProperty(MiddleNameProperty, data.MiddleName);
            LoadProperty(LastNameProperty, data.LastName);
            LoadProperty(NameSuffixProperty, data.NameSuffix);
            LoadProperty(TitleProperty, data.Title);
            LoadProperty(CompanyProperty, data.Company);
            LoadProperty(Address1Property, data.Address1);
            LoadProperty(Address2Property, data.Address2);
            LoadProperty(MunicipalityProperty, data.Municipality);
            LoadProperty(RegionProperty, data.Region);
            LoadProperty(PostalCodeProperty, data.PostalCode);
            LoadProperty(CountryProperty, data.Country);
            LoadProperty(EmailProperty, data.Email);
            LoadProperty(PhoneNumberProperty, data.PhoneNumber);
            LoadProperty(CompletedProperty, data.Completed);

        }
#endif

#endregion
    }
}
