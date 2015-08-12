using System;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using System.CompnonentModel.DataAnnotations;
#if SILVERLIGHT
using Csla.Serialization;
#else
using CoopCheck.DAL;
#endif

namespace CoopCheck.Library
{

    /// <summary>
    /// Account (editable root object).<br/>
    /// This is a generated base class of <see cref="Account"/> business object.
    /// </summary>
    [Serializable]
    public partial class Account : BusinessBase<Account>
    {

        #region Static Fields

        private static int _lastID;

        #endregion

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="Id"/> property.
        /// </summary>
        [NotUndoable]
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id, "Id");
        /// <summary>
        /// Gets the Id.
        /// </summary>
        /// <value>The Id.</value>
        public int Id
        {
            get { return GetProperty(IdProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Name"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name, "Name");
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Description"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description, "Description");
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Number"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> NumberProperty = RegisterProperty<string>(p => p.Number, "Number");
        /// <summary>
        /// Gets or sets the Number.
        /// </summary>
        /// <value>The Number.</value>
        public string Number
        {
            get { return GetProperty(NumberProperty); }
            set { SetProperty(NumberProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Balance"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal?> BalanceProperty = RegisterProperty<Decimal?>(p => p.Balance, "Balance");
        /// <summary>
        /// Gets or sets the Balance.
        /// </summary>
        /// <value>The Balance.</value>
        public Decimal? Balance
        {
            get { return GetProperty(BalanceProperty); }
            set { SetProperty(BalanceProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="LastReconciliationDate"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> LastReconciliationDateProperty = RegisterProperty<SmartDate>(p => p.LastReconciliationDate, "Last Reconciliation Date");
        /// <summary>
        /// Gets or sets the Last Reconciliation Date.
        /// </summary>
        /// <value>The Last Reconciliation Date.</value>
        public string LastReconciliationDate
        {
            get { return GetPropertyConvert<SmartDate, String>(LastReconciliationDateProperty); }
            set { SetPropertyConvert<SmartDate, String>(LastReconciliationDateProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="LastReconciliationBalance"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal?> LastReconciliationBalanceProperty = RegisterProperty<Decimal?>(p => p.LastReconciliationBalance, "Last Reconciliation Balance");
        /// <summary>
        /// Gets or sets the Last Reconciliation Balance.
        /// </summary>
        /// <value>The Last Reconciliation Balance.</value>
        public Decimal? LastReconciliationBalance
        {
            get { return GetProperty(LastReconciliationBalanceProperty); }
            set { SetProperty(LastReconciliationBalanceProperty, value); }
        }

        #endregion

        #region Factory Methods

#if !SILVERLIGHT

        /// <summary>
        /// Factory method. Creates a new <see cref="Account"/> object.
        /// </summary>
        /// <returns>A reference to the created <see cref="Account"/> object.</returns>
        public static Account NewAccount()
        {
            return DataPortal.Create<Account>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="Account"/> object, based on given parameters.
        /// </summary>
        /// <param name="id">The Id parameter of the Account to fetch.</param>
        /// <returns>A reference to the fetched <see cref="Account"/> object.</returns>
        public static Account GetAccount(int id)
        {
            return DataPortal.Fetch<Account>(id);
        }

        /// <summary>
        /// Factory method. Deletes a <see cref="Account"/> object, based on given parameters.
        /// </summary>
        /// <param name="id">The Id of the Account to delete.</param>
        public static void DeleteAccount(int id)
        {
            DataPortal.Delete<Account>(id);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="Account"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewAccount(EventHandler<DataPortalResult<Account>> callback)
        {
            DataPortal.BeginCreate<Account>(callback);
        }

#else

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="Account"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewAccount(EventHandler<DataPortalResult<Account>> callback)
        {
            DataPortal.BeginCreate<Account>(callback, DataPortal.ProxyModes.LocalOnly);
        }

#endif

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="Account"/> object, based on given parameters.
        /// </summary>
        /// <param name="id">The Id parameter of the Account to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetAccount(int id, EventHandler<DataPortalResult<Account>> callback)
        {
            DataPortal.BeginFetch<Account>(id, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously deletes a <see cref="Account"/> object, based on given parameters.
        /// </summary>
        /// <param name="id">The Id of the Account to delete.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void DeleteAccount(int id, EventHandler<DataPortalResult<Account>> callback)
        {
            DataPortal.BeginDelete<Account>(id, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Account()
#else
        private Account()
#endif
        {
            // Prevent direct creation
        }

        #endregion

        #region Object Authorization

        /// <summary>
        /// Adds the object authorization rules.
        /// </summary>
#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
#else
        protected static void AddObjectAuthorizationRules()
#endif
        {
            BusinessRules.AddRule(typeof (Account), new IsInRole(AuthorizationActions.CreateObject, "RECKNER\\CoopCheckAdmin"));
            BusinessRules.AddRule(typeof (Account), new IsInRole(AuthorizationActions.GetObject, "RECKNER\\CoopCheckReader"));
            BusinessRules.AddRule(typeof (Account), new IsInRole(AuthorizationActions.EditObject, "RECKNER\\CoopCheckAdmin"));
            BusinessRules.AddRule(typeof (Account), new IsInRole(AuthorizationActions.DeleteObject, "RECKNER\\CoopCheckAdmin"));

            AddObjectAuthorizationRulesExtend();
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
        #if DEBUG
            return true;
	#endif
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

        #region Data Access

#if !SILVERLIGHT

        /// <summary>
        /// Loads default values for the <see cref="Account"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void DataPortal_Create()
        {
            LoadProperty(IdProperty, System.Threading.Interlocked.Decrement(ref _lastID));
            LoadProperty(DescriptionProperty, null);
            LoadProperty(NumberProperty, null);
            LoadProperty(LastReconciliationDateProperty, null);
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.DataPortal_Create();
        }

        /// <summary>
        /// Loads a <see cref="Account"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="id">The Id.</param>
        protected void DataPortal_Fetch(int id)
        {
            var args = new DataPortalHookArgs(id);
            OnFetchPre(args);
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IAccountDal>();
                var data = dal.Fetch(id);
                Fetch(data);
            }
            OnFetchPost(args);
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        /// <summary>
        /// Loads a <see cref="Account"/> object from the given <see cref="AccountDto"/>.
        /// </summary>
        /// <param name="data">The AccountDto to use.</param>
        private void Fetch(AccountDto data)
        {
            // Value properties
            LoadProperty(IdProperty, data.Id);
            LoadProperty(NameProperty, data.Name);
            LoadProperty(DescriptionProperty, data.Description);
            LoadProperty(NumberProperty, data.Number);
            LoadProperty(BalanceProperty, data.Balance);
            LoadProperty(LastReconciliationDateProperty, data.LastReconciliationDate);
            LoadProperty(LastReconciliationBalanceProperty, data.LastReconciliationBalance);
            var args = new DataPortalHookArgs(data);
            OnFetchRead(args);
        }

        /// <summary>
        /// Inserts a new <see cref="Account"/> object in the database.
        /// </summary>
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            var dto = new AccountDto();
            dto.Name = Name;
            dto.Description = Description;
            dto.Number = Number;
            dto.Balance = Balance;
            dto.LastReconciliationDate = ReadProperty(LastReconciliationDateProperty);
            dto.LastReconciliationBalance = LastReconciliationBalance;
            using (var dalManager = DalFactory.GetManager())
            {
                var args = new DataPortalHookArgs(dto);
                OnInsertPre(args);
                var dal = dalManager.GetProvider<IAccountDal>();
                using (BypassPropertyChecks)
                {
                    var resultDto = dal.Insert(dto);
                    LoadProperty(IdProperty, resultDto.Id);
                    args = new DataPortalHookArgs(resultDto);
                }
                OnInsertPost(args);
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="Account"/> object.
        /// </summary>
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            var dto = new AccountDto();
            dto.Id = Id;
            dto.Name = Name;
            dto.Description = Description;
            dto.Number = Number;
            dto.Balance = Balance;
            dto.LastReconciliationDate = ReadProperty(LastReconciliationDateProperty);
            dto.LastReconciliationBalance = LastReconciliationBalance;
            using (var dalManager = DalFactory.GetManager())
            {
                var args = new DataPortalHookArgs(dto);
                OnUpdatePre(args);
                var dal = dalManager.GetProvider<IAccountDal>();
                using (BypassPropertyChecks)
                {
                    var resultDto = dal.Update(dto);
                    args = new DataPortalHookArgs(resultDto);
                }
                OnUpdatePost(args);
            }
        }

        /// <summary>
        /// Self deletes the <see cref="Account"/> object.
        /// </summary>
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(Id);
        }

        /// <summary>
        /// Deletes the <see cref="Account"/> object from database.
        /// </summary>
        /// <param name="id">The delete criteria.</param>
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(int id)
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var args = new DataPortalHookArgs();
                OnDeletePre(args);
                var dal = dalManager.GetProvider<IAccountDal>();
                using (BypassPropertyChecks)
                {
                    dal.Delete(id);
                }
                OnDeletePost(args);
            }
        }

#else

        /// <summary>
        /// Loads default values for the <see cref="Account"/> object properties.
        /// </summary>
        /// <param name="handler">The asynchronous handler.</param>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override void DataPortal_Create(Csla.DataPortalClient.LocalProxy<Account>.CompletedHandler handler)
        {
            LoadProperty(IdProperty, System.Threading.Interlocked.Decrement(ref _lastID));
            LoadProperty(DescriptionProperty, null);
            LoadProperty(NumberProperty, null);
            LoadProperty(LastReconciliationDateProperty, null);
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.DataPortal_Create(handler);
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
