using System;
using Csla;
#if SILVERLIGHT
using Csla.Serialization;
#else
using CoopCheck.DAL;
#endif

namespace CoopCheck.Library
{

    /// <summary>
    /// CheckInfo (read only object).<br/>
    /// This is a generated base class of <see cref="CheckInfo"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="CheckInfoList"/> collection.
    /// </remarks>
    [Serializable]
    public partial class CheckInfo : ReadOnlyBase<CheckInfo>
    {

        #region Static Fields

        private static int _lastID;

        #endregion

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="Id"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id, "Id", _lastID);
        /// <summary>
        /// Gets the Id.
        /// </summary>
        /// <value>The Id.</value>
        public int Id
        {
            get { return GetProperty(IdProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Date"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> DateProperty = RegisterProperty<SmartDate>(p => p.Date, "Date", null);
        /// <summary>
        /// Gets the Date.
        /// </summary>
        /// <value>The Date.</value>
        public string Date
        {
            get { return GetPropertyConvert<SmartDate, String>(DateProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="BatchNum"/> property.
        /// </summary>
        public static readonly PropertyInfo<int?> BatchNumProperty = RegisterProperty<int?>(p => p.BatchNum, "Batch Num", null);
        /// <summary>
        /// Gets the Batch Num.
        /// </summary>
        /// <value>The Batch Num.</value>
        public int? BatchNum
        {
            get { return GetProperty(BatchNumProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="AccountId"/> property.
        /// </summary>
        public static readonly PropertyInfo<int?> AccountIdProperty = RegisterProperty<int?>(p => p.AccountId, "Account Id", null);
        /// <summary>
        /// Gets the Account Id.
        /// </summary>
        /// <value>The Account Id.</value>
        public int? AccountId
        {
            get { return GetProperty(AccountIdProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="CheckNum"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> CheckNumProperty = RegisterProperty<string>(p => p.CheckNum, "Check Num", null);
        /// <summary>
        /// Gets the Check Num.
        /// </summary>
        /// <value>The Check Num.</value>
        public string CheckNum
        {
            get { return GetProperty(CheckNumProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Amount"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal?> AmountProperty = RegisterProperty<Decimal?>(p => p.Amount, "Amount", null);
        /// <summary>
        /// Gets the Amount.
        /// </summary>
        /// <value>The Amount.</value>
        public Decimal? Amount
        {
            get { return GetProperty(AmountProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="ClearedAmount"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal?> ClearedAmountProperty = RegisterProperty<Decimal?>(p => p.ClearedAmount, "Cleared Amount", null);
        /// <summary>
        /// Gets the Cleared Amount.
        /// </summary>
        /// <value>The Cleared Amount.</value>
        public Decimal? ClearedAmount
        {
            get { return GetProperty(ClearedAmountProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="ClearedDate"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> ClearedDateProperty = RegisterProperty<SmartDate>(p => p.ClearedDate, "Cleared Date", null);
        /// <summary>
        /// Gets the Cleared Date.
        /// </summary>
        /// <value>The Cleared Date.</value>
        public string ClearedDate
        {
            get { return GetPropertyConvert<SmartDate, String>(ClearedDateProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="IsPrinted"/> property.
        /// </summary>
        public static readonly PropertyInfo<bool> IsPrintedProperty = RegisterProperty<bool>(p => p.IsPrinted, "Is Printed");
        /// <summary>
        /// Gets the Is Printed.
        /// </summary>
        /// <value><c>true</c> if Is Printed; otherwise, <c>false</c>.</value>
        public bool IsPrinted
        {
            get { return GetProperty(IsPrintedProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="IsCleared"/> property.
        /// </summary>
        public static readonly PropertyInfo<bool> IsClearedProperty = RegisterProperty<bool>(p => p.IsCleared, "Is Cleared");
        /// <summary>
        /// Gets the Is Cleared.
        /// </summary>
        /// <value><c>true</c> if Is Cleared; otherwise, <c>false</c>.</value>
        public bool IsCleared
        {
            get { return GetProperty(IsClearedProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="PersonId"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> PersonIdProperty = RegisterProperty<string>(p => p.PersonId, "Person Id", null);
        /// <summary>
        /// Gets the Person Id.
        /// </summary>
        /// <value>The Person Id.</value>
        public string PersonId
        {
            get { return GetProperty(PersonIdProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Prefix"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> PrefixProperty = RegisterProperty<string>(p => p.Prefix, "Prefix", null);
        /// <summary>
        /// Gets the Prefix.
        /// </summary>
        /// <value>The Prefix.</value>
        public string Prefix
        {
            get { return GetProperty(PrefixProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="First"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> FirstProperty = RegisterProperty<string>(p => p.First, "First", null);
        /// <summary>
        /// Gets the First.
        /// </summary>
        /// <value>The First.</value>
        public string First
        {
            get { return GetProperty(FirstProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Middle"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> MiddleProperty = RegisterProperty<string>(p => p.Middle, "Middle", null);
        /// <summary>
        /// Gets the Middle.
        /// </summary>
        /// <value>The Middle.</value>
        public string Middle
        {
            get { return GetProperty(MiddleProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Last"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> LastProperty = RegisterProperty<string>(p => p.Last, "Last", null);
        /// <summary>
        /// Gets the Last.
        /// </summary>
        /// <value>The Last.</value>
        public string Last
        {
            get { return GetProperty(LastProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Suffix"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> SuffixProperty = RegisterProperty<string>(p => p.Suffix, "Suffix", null);
        /// <summary>
        /// Gets the Suffix.
        /// </summary>
        /// <value>The Suffix.</value>
        public string Suffix
        {
            get { return GetProperty(SuffixProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Title"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> TitleProperty = RegisterProperty<string>(p => p.Title, "Title", null);
        /// <summary>
        /// Gets the Title.
        /// </summary>
        /// <value>The Title.</value>
        public string Title
        {
            get { return GetProperty(TitleProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Company"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> CompanyProperty = RegisterProperty<string>(p => p.Company, "Company", null);
        /// <summary>
        /// Gets the Company.
        /// </summary>
        /// <value>The Company.</value>
        public string Company
        {
            get { return GetProperty(CompanyProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="AddressLine1"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> AddressLine1Property = RegisterProperty<string>(p => p.AddressLine1, "Address Line1", null);
        /// <summary>
        /// Gets the Address Line1.
        /// </summary>
        /// <value>The Address Line1.</value>
        public string AddressLine1
        {
            get { return GetProperty(AddressLine1Property); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="AddressLine2"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> AddressLine2Property = RegisterProperty<string>(p => p.AddressLine2, "Address Line2", null);
        /// <summary>
        /// Gets the Address Line2.
        /// </summary>
        /// <value>The Address Line2.</value>
        public string AddressLine2
        {
            get { return GetProperty(AddressLine2Property); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Municipality"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> MunicipalityProperty = RegisterProperty<string>(p => p.Municipality, "Municipality", null);
        /// <summary>
        /// Gets the Municipality.
        /// </summary>
        /// <value>The Municipality.</value>
        public string Municipality
        {
            get { return GetProperty(MunicipalityProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Region"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> RegionProperty = RegisterProperty<string>(p => p.Region, "Region", null);
        /// <summary>
        /// Gets the Region.
        /// </summary>
        /// <value>The Region.</value>
        public string Region
        {
            get { return GetProperty(RegionProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="PostalCode"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> PostalCodeProperty = RegisterProperty<string>(p => p.PostalCode, "Postal Code", null);
        /// <summary>
        /// Gets the Postal Code.
        /// </summary>
        /// <value>The Postal Code.</value>
        public string PostalCode
        {
            get { return GetProperty(PostalCodeProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Country"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> CountryProperty = RegisterProperty<string>(p => p.Country, "Country", null);
        /// <summary>
        /// Gets the Country.
        /// </summary>
        /// <value>The Country.</value>
        public string Country
        {
            get { return GetProperty(CountryProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="PhoneNumber"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> PhoneNumberProperty = RegisterProperty<string>(p => p.PhoneNumber, "Phone Number", null);
        /// <summary>
        /// Gets the Phone Number.
        /// </summary>
        /// <value>The Phone Number.</value>
        public string PhoneNumber
        {
            get { return GetProperty(PhoneNumberProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Email"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(p => p.Email, "Email", null);
        /// <summary>
        /// Gets the Email.
        /// </summary>
        /// <value>The Email.</value>
        public string Email
        {
            get { return GetProperty(EmailProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Updated"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> UpdatedProperty = RegisterProperty<SmartDate>(p => p.Updated, "Updated", null);
        /// <summary>
        /// Gets the Updated.
        /// </summary>
        /// <value>The Updated.</value>
        public string Updated
        {
            get { return GetPropertyConvert<SmartDate, String>(UpdatedProperty); }
        }

        #endregion

        #region Factory Methods

#if !SILVERLIGHT

        /// <summary>
        /// Factory method. Loads a <see cref="CheckInfo"/> object from the given CheckInfoDto.
        /// </summary>
        /// <param name="data">The <see cref="CheckInfoDto"/>.</param>
        /// <returns>A reference to the fetched <see cref="CheckInfo"/> object.</returns>
        internal static CheckInfo GetCheckInfo(CheckInfoDto data)
        {
            CheckInfo obj = new CheckInfo();
            obj.Fetch(data);
            return obj;
        }

#endif

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckInfo"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public CheckInfo()
#else
        private CheckInfo()
#endif
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

#if !SILVERLIGHT

        /// <summary>
        /// Loads a <see cref="CheckInfo"/> object from the given <see cref="CheckInfoDto"/>.
        /// </summary>
        /// <param name="data">The CheckInfoDto to use.</param>
        private void Fetch(CheckInfoDto data)
        {
            // Value properties
            LoadProperty(IdProperty, data.Id);
            LoadProperty(DateProperty, data.Date);
            LoadProperty(BatchNumProperty, data.BatchNum);
            LoadProperty(AccountIdProperty, data.AccountId);
            LoadProperty(CheckNumProperty, data.CheckNum);
            LoadProperty(AmountProperty, data.Amount);
            LoadProperty(ClearedAmountProperty, data.ClearedAmount);
            LoadProperty(ClearedDateProperty, data.ClearedDate);
            LoadProperty(IsPrintedProperty, data.IsPrinted);
            LoadProperty(IsClearedProperty, data.IsCleared);
            LoadProperty(PersonIdProperty, data.PersonId);
            LoadProperty(PrefixProperty, data.Prefix);
            LoadProperty(FirstProperty, data.First);
            LoadProperty(MiddleProperty, data.Middle);
            LoadProperty(LastProperty, data.Last);
            LoadProperty(SuffixProperty, data.Suffix);
            LoadProperty(TitleProperty, data.Title);
            LoadProperty(CompanyProperty, data.Company);
            LoadProperty(AddressLine1Property, data.AddressLine1);
            LoadProperty(AddressLine2Property, data.AddressLine2);
            LoadProperty(MunicipalityProperty, data.Municipality);
            LoadProperty(RegionProperty, data.Region);
            LoadProperty(PostalCodeProperty, data.PostalCode);
            LoadProperty(CountryProperty, data.Country);
            LoadProperty(PhoneNumberProperty, data.PhoneNumber);
            LoadProperty(EmailProperty, data.Email);
            LoadProperty(UpdatedProperty, data.Updated);
            var args = new DataPortalHookArgs(data);
            OnFetchRead(args);
        }

#endif

        #endregion

        #region Pseudo Events

#if !SILVERLIGHT

        /// <summary>
        /// Occurs after the low level fetch operation, before the data reader is destroyed.
        /// </summary>
        partial void OnFetchRead(DataPortalHookArgs args);

#endif

        #endregion

    }
}
