using System;
using Csla;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using CoopCheck.Library.Contracts.Interfaces;
#if SILVERLIGHT
using Csla.Serialization;
#else
using CoopCheck.DAL;
#endif

namespace CoopCheck.Library
{

    /// <summary>
    /// VoucherEdit (editable child object).<br/>
    /// This is a generated base class of <see cref="VoucherEdit"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="VoucherList"/> collection.
    /// </remarks>
    [Serializable]
    public partial class VoucherEdit : BusinessBase<VoucherEdit>, IVoucherEdit
    {

        #region Static Fields

        private static int _lastID;

        #endregion

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="Id"/> property.
        /// </summary>
        [NotUndoable]
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id, "Id" );
        /// <summary>
        /// Gets the Id.
        /// </summary>
        /// <value>The Id.</value>
        public int Id
        {
            get { return GetProperty(IdProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Amount"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal?> AmountProperty = RegisterProperty<Decimal?>(p => p.Amount, "Amount");
        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        /// <value>The Amount.</value>
        [Display(Name="Voucher Amount"), Range(-5000,5000)]
        public Decimal? Amount
        {
            get { return GetProperty(AmountProperty); }
            set { SetProperty(AmountProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="PersonId"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> PersonIdProperty = RegisterProperty<string>(p => p.PersonId, "Person Id");
        /// <summary>
        /// Gets or sets the Person Id.
        /// </summary>
        /// <value>The Person Id.</value>
        [StringLength(50)]
        public string PersonId
        {
            get { return GetProperty(PersonIdProperty); }
            set { SetProperty(PersonIdProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="NamePrefix"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> NamePrefixProperty = RegisterProperty<string>(p => p.NamePrefix, "Name Prefix");
        /// <summary>
        /// Gets or sets the Name Prefix.
        /// </summary>
        /// <value>The Name Prefix.</value>
        [StringLength(5)]
        public string NamePrefix
        {
            get { return GetProperty(NamePrefixProperty); }
            set { SetProperty(NamePrefixProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="First"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> FirstProperty = RegisterProperty<string>(p => p.First, "First");
        /// <summary>
        /// Gets or sets the First.
        /// </summary>
        /// <value>The First.</value>
        [Display(Name="First Name"), StringLength(20)]
        public string First
        {
            get { return GetProperty(FirstProperty); }
            set { SetProperty(FirstProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Middle"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> MiddleProperty = RegisterProperty<string>(p => p.Middle, "Middle");
        /// <summary>
        /// Gets or sets the Middle.
        /// </summary>
        /// <value>The Middle.</value>
        [Display(Name="Middle Name"), StringLength(20)]
        public string Middle
        {
            get { return GetProperty(MiddleProperty); }
            set { SetProperty(MiddleProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Last"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> LastProperty = RegisterProperty<string>(p => p.Last, "Last");
        /// <summary>
        /// Gets or sets the Last.
        /// </summary>
        /// <value>The Last.</value>
        [Display(Name="Last Name"), StringLength(20)]
        public string Last
        {
            get { return GetProperty(LastProperty); }
            set { SetProperty(LastProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Suffix"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> SuffixProperty = RegisterProperty<string>(p => p.Suffix, "Suffix");
        /// <summary>
        /// Gets or sets the Suffix.
        /// </summary>
        /// <value>The Suffix.</value>
        [Display(Name="Name Suffix"), StringLength(15)]
        public string Suffix
        {
            get { return GetProperty(SuffixProperty); }
            set { SetProperty(SuffixProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Title"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> TitleProperty = RegisterProperty<string>(p => p.Title, "Title");
        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        /// <value>The Title.</value>
        [StringLength(35)]
        public string Title
        {
            get { return GetProperty(TitleProperty); }
            set { SetProperty(TitleProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Company"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> CompanyProperty = RegisterProperty<string>(p => p.Company, "Company");
        /// <summary>
        /// Gets or sets the Company.
        /// </summary>
        /// <value>The Company.</value>
        [StringLength(35)]
        public string Company
        {
            get { return GetProperty(CompanyProperty); }
            set { SetProperty(CompanyProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="AddressLine1"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> AddressLine1Property = RegisterProperty<string>(p => p.AddressLine1, "Address Line1");
        /// <summary>
        /// Gets or sets the Address Line1.
        /// </summary>
        /// <value>The Address Line1.</value>
        [StringLength(50)]
        [Required]
        public string AddressLine1
        {
            get { return GetProperty(AddressLine1Property); }
            set { SetProperty(AddressLine1Property, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="AddressLine2"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> AddressLine2Property = RegisterProperty<string>(p => p.AddressLine2, "Address Line2");
        /// <summary>
        /// Gets or sets the Address Line2.
        /// </summary>
        /// <value>The Address Line2.</value>
        [StringLength(50)]
        public string AddressLine2
        {
            get { return GetProperty(AddressLine2Property); }
            set { SetProperty(AddressLine2Property, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Municipality"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> MunicipalityProperty = RegisterProperty<string>(p => p.Municipality, "Municipality");
        /// <summary>
        /// Gets or sets the Municipality.
        /// </summary>
        /// <value>The Municipality.</value>
        [StringLength(35)]
        [Required]
        public string Municipality
        {
            get { return GetProperty(MunicipalityProperty); }
            set { SetProperty(MunicipalityProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Region"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> RegionProperty = RegisterProperty<string>(p => p.Region, "Region");
        /// <summary>
        /// Gets or sets the Region.
        /// </summary>
        /// <value>The Region.</value>
        [StringLength(35)]
        [Required]
        public string Region
        {
            get { return GetProperty(RegionProperty); }
            set { SetProperty(RegionProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="PostalCode"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> PostalCodeProperty = RegisterProperty<string>(p => p.PostalCode, "Postal Code");
        /// <summary>
        /// Gets or sets the Postal Code.
        /// </summary>
        /// <value>The Postal Code.</value>
        [StringLength(15)]
        [DataType(DataType.PostalCode)]
        public string PostalCode
        {
            get { return GetProperty(PostalCodeProperty); }
            set { SetProperty(PostalCodeProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Country"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> CountryProperty = RegisterProperty<string>(p => p.Country, "Country");
        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        /// <value>The Country.</value>
        [StringLength(2)]
        public string Country
        {
            get { return GetProperty(CountryProperty); }
            set { SetProperty(CountryProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="PhoneNumber"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> PhoneNumberProperty = RegisterProperty<string>(p => p.PhoneNumber, "Phone Number");
        /// <summary>
        /// Gets or sets the Phone Number.
        /// </summary>
        /// <value>The Phone Number.</value>
        [StringLength(22)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber
        {
            get { return GetProperty(PhoneNumberProperty); }
            set { SetProperty(PhoneNumberProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="EmailAddress"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> EmailAddressProperty = RegisterProperty<string>(p => p.EmailAddress, "Email Address");
        /// <summary>
        /// Gets or sets the Email Address.
        /// </summary>
        /// <value>The Email Address.</value>
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress
        {
            get { return GetProperty(EmailAddressProperty); }
            set { SetProperty(EmailAddressProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Updated"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> UpdatedProperty = RegisterProperty<SmartDate>(p => p.Updated, "Updated");
        /// <summary>
        /// Gets or sets the Updated.
        /// </summary>
        /// <value>The Updated.</value>
        public string Updated
        {
            get { return GetPropertyConvert<SmartDate, String>(UpdatedProperty); }
            set { SetPropertyConvert<SmartDate, String>(UpdatedProperty, value); }
        }

        #endregion

        // TODO: add public properties and methods

     
        #region Factory Methods

#if !SILVERLIGHT

        /// <summary>
        /// Factory method. Creates a new <see cref="VoucherEdit"/> object.
        /// </summary>
        /// <returns>A reference to the created <see cref="VoucherEdit"/> object.</returns>
        public static VoucherEdit NewVoucherEdit()
        {
            return DataPortal.CreateChild<VoucherEdit>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="VoucherEdit"/> object from the given VoucherEditDto.
        /// </summary>
        /// <param name="data">The <see cref="VoucherEditDto"/>.</param>
        /// <returns>A reference to the fetched <see cref="VoucherEdit"/> object.</returns>
        internal static VoucherEdit GetVoucherEdit(VoucherEditDto data)
        {
            VoucherEdit obj = new VoucherEdit();
            // show the framework that this is a child object
            obj.MarkAsChild();
            obj.Fetch(data);
            obj.MarkOld();
            // check all object rules and property rules
            obj.BusinessRules.CheckRules();
            return obj;
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="VoucherEdit"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        internal static void NewVoucherEdit(EventHandler<DataPortalResult<VoucherEdit>> callback)
        {
            DataPortal.BeginCreate<VoucherEdit>(callback);
        }
        

#else

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="VoucherEdit"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        internal static void NewVoucherEdit(EventHandler<DataPortalResult<VoucherEdit>> callback)
        {
            DataPortal.BeginCreate<VoucherEdit>(callback, DataPortal.ProxyModes.LocalOnly);
        }

#endif

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VoucherEdit"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public VoucherEdit()
#else
        private VoucherEdit()
#endif
        {
            // Prevent direct creation

            // show the framework that this is a child object
            MarkAsChild();
        }

        #endregion

        #region Data Access

#if !SILVERLIGHT

        /// <summary>
        /// Loads default values for the <see cref="VoucherEdit"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void Child_Create()
        {
            LoadProperty(IdProperty, System.Threading.Interlocked.Decrement(ref _lastID));
            LoadProperty(PersonIdProperty, string.Empty);
            LoadProperty(NamePrefixProperty, string.Empty);
            LoadProperty(FirstProperty, string.Empty);
            LoadProperty(MiddleProperty, string.Empty);
            LoadProperty(LastProperty, string.Empty);
            LoadProperty(SuffixProperty, string.Empty);
            LoadProperty(TitleProperty, string.Empty);
            LoadProperty(CompanyProperty, string.Empty);
            LoadProperty(AddressLine1Property, string.Empty);
            LoadProperty(AddressLine2Property, string.Empty);
            LoadProperty(MunicipalityProperty, string.Empty);
            LoadProperty(RegionProperty, string.Empty);
            LoadProperty(PostalCodeProperty, string.Empty);
            LoadProperty(CountryProperty, ConfigurationManager.AppSettings["DefaultCountry"]);
            LoadProperty(PhoneNumberProperty, string.Empty);
            LoadProperty(EmailAddressProperty, string.Empty);
            LoadProperty(UpdatedProperty, DateTime.Now);
            MarkClean();
            
        }

        /// <summary>
        /// Loads a <see cref="VoucherEdit"/> object from the given <see cref="VoucherEditDto"/>.
        /// </summary>
        /// <param name="data">The VoucherEditDto to use.</param>
        private void Fetch(VoucherEditDto data)
        {
            // Value properties
            LoadProperty(IdProperty, data.Id);
            LoadProperty(AmountProperty, data.Amount);
            LoadProperty(PersonIdProperty, data.PersonId);
            LoadProperty(NamePrefixProperty, data.NamePrefix);
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
            LoadProperty(EmailAddressProperty, data.EmailAddress);
            LoadProperty(UpdatedProperty, data.Updated);
            var args = new DataPortalHookArgs(data);
            OnFetchRead(args);
        }

        /// <summary>
        /// Inserts a new <see cref="VoucherEdit"/> object in the database.
        /// </summary>
        /// <param name="parent">The parent object.</param>
        [Transactional(TransactionalTypes.TransactionScope)]
        private void Child_Insert(BatchEdit parent)
        {
            var dto = new VoucherEditDto();
            dto.Parent_Num = parent.Num;
            dto.Amount = Amount;
            dto.PersonId = PersonId;
            dto.NamePrefix = NamePrefix;
            dto.First = First;
            dto.Middle = Middle;
            dto.Last = Last;
            dto.Suffix = Suffix;
            dto.Title = Title;
            dto.Company = Company;
            dto.AddressLine1 = AddressLine1;
            dto.AddressLine2 = AddressLine2;
            dto.Municipality = Municipality;
            dto.Region = Region;
            dto.PostalCode = PostalCode;
            dto.Country = Country;
            dto.PhoneNumber = PhoneNumber;
            dto.EmailAddress = EmailAddress;
            dto.Updated = ReadProperty(UpdatedProperty);
            using (var dalManager = DalFactory.GetManager())
            {
                var args = new DataPortalHookArgs(dto);
                OnInsertPre(args);
                var dal = dalManager.GetProvider<IVoucherEditDal>();
                using (BypassPropertyChecks)
                {
                    var resultDto = dal.Insert(dto);
                    args = new DataPortalHookArgs(resultDto);
                }
                OnInsertPost(args);
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="VoucherEdit"/> object.
        /// </summary>
        [Transactional(TransactionalTypes.TransactionScope)]
        private void Child_Update()
        {
            if (!IsDirty)
                return;

            var dto = new VoucherEditDto();
            dto.Id = Id;
            dto.Amount = Amount;
            dto.PersonId = PersonId;
            dto.NamePrefix = NamePrefix;
            dto.First = First;
            dto.Middle = Middle;
            dto.Last = Last;
            dto.Suffix = Suffix;
            dto.Title = Title;
            dto.Company = Company;
            dto.AddressLine1 = AddressLine1;
            dto.AddressLine2 = AddressLine2;
            dto.Municipality = Municipality;
            dto.Region = Region;
            dto.PostalCode = PostalCode;
            dto.Country = Country;
            dto.PhoneNumber = PhoneNumber;
            dto.EmailAddress = EmailAddress;
            dto.Updated = ReadProperty(UpdatedProperty);
            using (var dalManager = DalFactory.GetManager())
            {
                var args = new DataPortalHookArgs(dto);
                OnUpdatePre(args);
                var dal = dalManager.GetProvider<IVoucherEditDal>();
                using (BypassPropertyChecks)
                {
                    var resultDto = dal.Update(dto);
                    args = new DataPortalHookArgs(resultDto);
                }
                OnUpdatePost(args);
            }
        }

        /// <summary>
        /// Self deletes the <see cref="VoucherEdit"/> object from database.
        /// </summary>
        [Transactional(TransactionalTypes.TransactionScope)]
        private void Child_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var args = new DataPortalHookArgs();
                OnDeletePre(args);
                var dal = dalManager.GetProvider<IVoucherEditDal>();
                using (BypassPropertyChecks)
                {
                    dal.Delete(ReadProperty(IdProperty));
                }
                OnDeletePost(args);
            }
        }

#else

        /// <summary>
        /// Loads default values for the <see cref="VoucherEdit"/> object properties.
        /// </summary>
        /// <param name="handler">The asynchronous handler.</param>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public void Child_Create(Csla.DataPortalClient.LocalProxy<VoucherEdit>.CompletedHandler handler)
        {
            LoadProperty(IdProperty, System.Threading.Interlocked.Decrement(ref _lastID));
            LoadProperty(PersonIdProperty, null);
            LoadProperty(NamePrefixProperty, null);
            LoadProperty(FirstProperty, null);
            LoadProperty(MiddleProperty, null);
            LoadProperty(LastProperty, null);
            LoadProperty(SuffixProperty, null);
            LoadProperty(TitleProperty, null);
            LoadProperty(CompanyProperty, null);
            LoadProperty(AddressLine1Property, null);
            LoadProperty(AddressLine2Property, null);
            LoadProperty(MunicipalityProperty, null);
            LoadProperty(RegionProperty, null);
            LoadProperty(PostalCodeProperty, null);
            LoadProperty(CountryProperty, null);
            LoadProperty(PhoneNumberProperty, null);
            LoadProperty(EmailAddressProperty, null);
            LoadProperty(UpdatedProperty, null);
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.Child_Create();
        }

#endif

        #endregion

        #region Pseudo Events

        /// <summary>
        /// Occurs after setting all defaults for object creation.
        /// </summary>
        partial void OnCreate(DataPortalHookArgs args);

#if !SILVERLIGHT

        /// <summary>
        /// Occurs in DataPortal_Delete, after setting query parameters and before the delete operation.
        /// </summary>
        partial void OnDeletePre(DataPortalHookArgs args);

        /// <summary>
        /// Occurs in DataPortal_Delete, after the delete operation, before Commit().
        /// </summary>
        partial void OnDeletePost(DataPortalHookArgs args);

        /// <summary>
        /// Occurs after setting query parameters and before the fetch operation.
        /// </summary>
        partial void OnFetchPre(DataPortalHookArgs args);

        /// <summary>
        /// Occurs after the fetch operation (object or collection is fully loaded and set up).
        /// </summary>
        partial void OnFetchPost(DataPortalHookArgs args);

        /// <summary>
        /// Occurs after the low level fetch operation, before the data reader is destroyed.
        /// </summary>
        partial void OnFetchRead(DataPortalHookArgs args);

        /// <summary>
        /// Occurs after setting query parameters and before the update operation.
        /// </summary>
        partial void OnUpdatePre(DataPortalHookArgs args);

        /// <summary>
        /// Occurs in DataPortal_Insert, after the update operation, before setting back row identifiers (RowVersion) and Commit().
        /// </summary>
        partial void OnUpdatePost(DataPortalHookArgs args);

        /// <summary>
        /// Occurs in DataPortal_Insert, after setting query parameters and before the insert operation.
        /// </summary>
        partial void OnInsertPre(DataPortalHookArgs args);

        /// <summary>
        /// Occurs in DataPortal_Insert, after the insert operation, before setting back row identifiers (ID and RowVersion) and Commit().
        /// </summary>
        partial void OnInsertPost(DataPortalHookArgs args);

#endif

        #endregion

    }
}
