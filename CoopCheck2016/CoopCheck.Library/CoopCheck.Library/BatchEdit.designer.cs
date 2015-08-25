using System;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
#if SILVERLIGHT
using Csla.Serialization;
#else
using CoopCheck.DAL;
#endif

namespace CoopCheck.Library
{

    /// <summary>
    /// BatchEdit (editable root object).<br/>
    /// This is a generated base class of <see cref="BatchEdit"/> business object.
    /// </summary>
    /// <remarks>
    /// This class contains one child collection:<br/>
    /// - <see cref="Vouchers"/> of type <see cref="VoucherList"/> (1:M relation to <see cref="VoucherEdit"/>)
    /// </remarks>
    [Serializable]
    public partial class BatchEdit : BusinessBase<BatchEdit>
    {

        #region Static Fields

        private static int _lastID;

        #endregion

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="Num"/> property.
        /// </summary>
        [NotUndoable]
        public static readonly PropertyInfo<int> NumProperty = RegisterProperty<int>(p => p.Num, "Num");
        /// <summary>
        /// Gets the Num.
        /// </summary>
        /// <value>The Num.</value>
        public int Num
        {
            get { return GetProperty(NumProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Date"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> DateProperty = RegisterProperty<SmartDate>(p => p.Date, "Date");
        /// <summary>
        /// Gets or sets the Date.
        /// </summary>
        /// <value>The Date.</value>
        public string Date
        {
            get { return GetPropertyConvert<SmartDate, String>(DateProperty); }
            set { SetPropertyConvert<SmartDate, String>(DateProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="PayDate"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> PayDateProperty = RegisterProperty<SmartDate>(p => p.PayDate, "Pay Date");
        /// <summary>
        /// Gets or sets the Pay Date.
        /// </summary>
        /// <value>The Pay Date.</value>
        public string PayDate
        {
            get { return GetPropertyConvert<SmartDate, String>(PayDateProperty); }
            set { SetPropertyConvert<SmartDate, String>(PayDateProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Amount"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal?> AmountProperty = RegisterProperty<Decimal?>(p => p.Amount, "Amount");
        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        /// <value>The Amount.</value>
        public Decimal? Amount
        {
            get { return GetProperty(AmountProperty); }
            set { SetProperty(AmountProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="JobNum"/> property.
        /// </summary>
        public static readonly PropertyInfo<int?> JobNumProperty = RegisterProperty<int?>(p => p.JobNum, "Job Num");
        /// <summary>
        /// Gets or sets the Job Num.
        /// </summary>
        /// <value>The Job Num.</value>
        public int? JobNum
        {
            get { return GetProperty(JobNumProperty); }
            set { SetProperty(JobNumProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Description"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description, "Description");
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        [StringLength(255)]
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
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

        /// <summary>
        /// Maintains metadata about <see cref="ThankYou1"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> ThankYou1Property = RegisterProperty<string>(p => p.ThankYou1, "Thank You1");
        /// <summary>
        /// Gets or sets the Thank You1.
        /// </summary>
        /// <value>The Thank You1.</value>
        [StringLength(100)]
        public string ThankYou1
        {
            get { return GetProperty(ThankYou1Property); }
            set { SetProperty(ThankYou1Property, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="StudyTopic"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> StudyTopicProperty = RegisterProperty<string>(p => p.StudyTopic, "Study Topic");
        /// <summary>
        /// Gets or sets the Study Topic.
        /// </summary>
        /// <value>The Study Topic.</value>
        [StringLength(150)]
        public string StudyTopic
        {
            get { return GetProperty(StudyTopicProperty); }
            set { SetProperty(StudyTopicProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="ThankYou2"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> ThankYou2Property = RegisterProperty<string>(p => p.ThankYou2, "Thank You2");
        /// <summary>
        /// Gets or sets the Thank You2.
        /// </summary>
        /// <value>The Thank You2.</value>
        [StringLength(150)]
        public string ThankYou2
        {
            get { return GetProperty(ThankYou2Property); }
            set { SetProperty(ThankYou2Property, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MarketingResearchMessage"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> MarketingResearchMessageProperty = RegisterProperty<string>(p => p.MarketingResearchMessage, "Marketing Research Message");
        /// <summary>
        /// Gets or sets the Marketing Research Message.
        /// </summary>
        /// <value>The Marketing Research Message.</value>
        [StringLength(500)]
        public string MarketingResearchMessage
        {
            get { return GetProperty(MarketingResearchMessageProperty); }
            set { SetProperty(MarketingResearchMessageProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about child <see cref="Vouchers"/> property.
        /// </summary>
        public static readonly PropertyInfo<VoucherList> VouchersProperty = RegisterProperty<VoucherList>(p => p.Vouchers, "Vouchers", RelationshipTypes.Child);
        /// <summary>
        /// Gets the Vouchers ("parent load" child property).
        /// </summary>
        /// <value>The Vouchers.</value>
        public VoucherList Vouchers
        {
            get { return GetProperty(VouchersProperty); }
            private set { LoadProperty(VouchersProperty, value); }
        }

        #endregion

        #region Factory Methods

#if !SILVERLIGHT

        /// <summary>
        /// Factory method. Creates a new <see cref="BatchEdit"/> object.
        /// </summary>
        /// <returns>A reference to the created <see cref="BatchEdit"/> object.</returns>
        public static BatchEdit NewBatchEdit()
        {
            return DataPortal.Create<BatchEdit>();
        }



        /// <summary>
        /// Factory method. Loads a <see cref="BatchEdit"/> object, based on given parameters.
        /// </summary>
        /// <param name="batch_num">The Batch_num parameter of the BatchEdit to fetch.</param>
        /// <returns>A reference to the fetched <see cref="BatchEdit"/> object.</returns>
        public static BatchEdit GetBatchEdit(int batch_num)
        {
            return DataPortal.Fetch<BatchEdit>(batch_num);
        }

        /// <summary>
        /// Factory method. Deletes a <see cref="BatchEdit"/> object, based on given parameters.
        /// </summary>
        /// <param name="batch_num">The Batch_num of the BatchEdit to delete.</param>
        public static void DeleteBatchEdit(int batch_num)
        {
            DataPortal.Delete<BatchEdit>(batch_num);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="BatchEdit"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewBatchEdit(EventHandler<DataPortalResult<BatchEdit>> callback)
        {
            DataPortal.BeginCreate<BatchEdit>(callback);
        }

#else

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="BatchEdit"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewBatchEdit(EventHandler<DataPortalResult<BatchEdit>> callback)
        {
            DataPortal.BeginCreate<BatchEdit>(callback, DataPortal.ProxyModes.LocalOnly);
        }

#endif

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BatchEdit"/> object, based on given parameters.
        /// </summary>
        /// <param name="batch_num">The Batch_num parameter of the BatchEdit to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBatchEdit(int batch_num, EventHandler<DataPortalResult<BatchEdit>> callback)
        {
            DataPortal.BeginFetch<BatchEdit>(batch_num, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously deletes a <see cref="BatchEdit"/> object, based on given parameters.
        /// </summary>
        /// <param name="batch_num">The Batch_num of the BatchEdit to delete.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void DeleteBatchEdit(int batch_num, EventHandler<DataPortalResult<BatchEdit>> callback)
        {
            DataPortal.BeginDelete<BatchEdit>(batch_num, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchEdit"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public BatchEdit()
#else
        private BatchEdit()
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
#if RELEASE
            BusinessRules.AddRule(typeof(BatchEdit), new IsInRole(AuthorizationActions.CreateObject, "RECKNER\\CoopCheckAdmin"));
            BusinessRules.AddRule(typeof(BatchEdit), new IsInRole(AuthorizationActions.GetObject, "RECKNER\\CoopCheckReader"));
            BusinessRules.AddRule(typeof(BatchEdit), new IsInRole(AuthorizationActions.EditObject, "RECKNER\\CoopCheckAdmin"));
            BusinessRules.AddRule(typeof(BatchEdit), new IsInRole(AuthorizationActions.DeleteObject, "RECKNER\\CoopCheckAdmin"));
            AddObjectAuthorizationRulesExtend();
#endif

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
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.CreateObject, typeof(BatchEdit));
        }

        /// <summary>
        /// Checks if the current user can retrieve Account's properties.
        /// </summary>
        /// <returns><c>true</c> if the user can read the object; otherwise, <c>false</c>.</returns>
        public static bool CanGetObject()
        {
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.GetObject, typeof(BatchEdit));
        }

        /// <summary>
        /// Checks if the current user can change Account's properties.
        /// </summary>
        /// <returns><c>true</c> if the user can update the object; otherwise, <c>false</c>.</returns>
        public static bool CanEditObject()
        {
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.EditObject, typeof(BatchEdit));
        }

        /// <summary>
        /// Checks if the current user can delete a Account object.
        /// </summary>
        /// <returns><c>true</c> if the user can delete the object; otherwise, <c>false</c>.</returns>
        public static bool CanDeleteObject()
        {
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.DeleteObject, typeof(BatchEdit));
        }

        #endregion
        #region Data Access

#if !SILVERLIGHT

        /// <summary>
        /// Loads default values for the <see cref="BatchEdit"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void DataPortal_Create()
        {
            LoadProperty(NumProperty, System.Threading.Interlocked.Decrement(ref _lastID));
            LoadProperty(DateProperty, DateTime.Today);
            LoadProperty(PayDateProperty, DateTime.Today.AddDays(((int)DayOfWeek.Friday - (int)DateTime.Today.DayOfWeek + 7) % 7));
            LoadProperty(DescriptionProperty, null);
            LoadProperty(UpdatedProperty, null);
            LoadProperty(ThankYou1Property, ConfigurationManager.AppSettings["ThankYou1"]);
            LoadProperty(StudyTopicProperty, ConfigurationManager.AppSettings["StudyTopic"]);
            LoadProperty(ThankYou2Property, ConfigurationManager.AppSettings["ThankYou2"]);
            LoadProperty(MarketingResearchMessageProperty, ConfigurationManager.AppSettings["MarketingResearchMessage"]);
            LoadProperty(VouchersProperty, DataPortal.CreateChild<VoucherList>());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.DataPortal_Create();
        }

        /// <summary>
        /// Loads a <see cref="BatchEdit"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="batch_num">The batch_num.</param>
        protected void DataPortal_Fetch(int batch_num)
        {
            var args = new DataPortalHookArgs(batch_num);
            OnFetchPre(args);
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IBatchEditDal>();
                var data = dal.Fetch(batch_num);
                Fetch(data);
                FetchChildren(dal);
            }
            OnFetchPost(args);
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        /// <summary>
        /// Loads a <see cref="BatchEdit"/> object from the given <see cref="BatchEditDto"/>.
        /// </summary>
        /// <param name="data">The BatchEditDto to use.</param>
        private void Fetch(BatchEditDto data)
        {
            // Value properties
            LoadProperty(NumProperty, data.Num);
            LoadProperty(DateProperty, data.Date);
            LoadProperty(PayDateProperty, data.PayDate);
            LoadProperty(AmountProperty, data.Amount);
            LoadProperty(JobNumProperty, data.JobNum);
            LoadProperty(DescriptionProperty, data.Description);
            LoadProperty(UpdatedProperty, data.Updated);
            LoadProperty(ThankYou1Property, data.ThankYou1);
            LoadProperty(StudyTopicProperty, data.StudyTopic);
            LoadProperty(ThankYou2Property, data.ThankYou2);
            LoadProperty(MarketingResearchMessageProperty, data.MarketingResearchMessage);
            var args = new DataPortalHookArgs(data);
            OnFetchRead(args);
        }

        /// <summary>
        /// Loads child objects from the given DAL provider.
        /// </summary>
        /// <param name="dal">The DAL provider to use.</param>
        private void FetchChildren(IBatchEditDal dal)
        {
            LoadProperty(VouchersProperty, VoucherList.GetVoucherList(dal.VoucherList));
        }

        /// <summary>
        /// Inserts a new <see cref="BatchEdit"/> object in the database.
        /// </summary>
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            var dto = new BatchEditDto();
            dto.Date = ReadProperty(DateProperty);
            dto.PayDate = ReadProperty(PayDateProperty);
            dto.Amount = Amount;
            dto.JobNum = JobNum;
            dto.Description = Description;
            dto.ThankYou1 = ThankYou1;
            dto.StudyTopic = StudyTopic;
            dto.ThankYou2 = ThankYou2;
            dto.MarketingResearchMessage = MarketingResearchMessage;
            using (var dalManager = DalFactory.GetManager())
            {
                var args = new DataPortalHookArgs(dto);
                OnInsertPre(args);
                var dal = dalManager.GetProvider<IBatchEditDal>();
                using (BypassPropertyChecks)
                {
                    var resultDto = dal.Insert(dto);
                    LoadProperty(NumProperty, resultDto.Num);
                    args = new DataPortalHookArgs(resultDto);
                }
                OnInsertPost(args);
                // flushes all pending data operations
                FieldManager.UpdateChildren(this);
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="BatchEdit"/> object.
        /// </summary>
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            var dto = new BatchEditDto();
            dto.Num = Num;
            dto.Date = ReadProperty(DateProperty);
            dto.PayDate = ReadProperty(PayDateProperty);
            dto.Amount = Amount;
            dto.JobNum = JobNum;
            dto.Description = Description;
            dto.ThankYou1 = ThankYou1;
            dto.StudyTopic = StudyTopic;
            dto.ThankYou2 = ThankYou2;
            dto.MarketingResearchMessage = MarketingResearchMessage;
            using (var dalManager = DalFactory.GetManager())
            {
                var args = new DataPortalHookArgs(dto);
                OnUpdatePre(args);
                var dal = dalManager.GetProvider<IBatchEditDal>();
                using (BypassPropertyChecks)
                {
                    var resultDto = dal.Update(dto);
                    args = new DataPortalHookArgs(resultDto);
                }
                OnUpdatePost(args);
                // flushes all pending data operations
                FieldManager.UpdateChildren(this);
            }
        }

        /// <summary>
        /// Self deletes the <see cref="BatchEdit"/> object.
        /// </summary>
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(this.Num);
        }

        /// <summary>
        /// Deletes the <see cref="BatchEdit"/> object from database.
        /// </summary>
        /// <param name="batch_num">The delete criteria.</param>
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(int batch_num)
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var args = new DataPortalHookArgs();
                // flushes all pending data operations
                FieldManager.UpdateChildren(this);
                OnDeletePre(args);
                var dal = dalManager.GetProvider<IBatchEditDal>();
                using (BypassPropertyChecks)
                {
                    dal.Delete(batch_num);
                }
                OnDeletePost(args);
            }
        }

#else

        /// <summary>
        /// Loads default values for the <see cref="BatchEdit"/> object properties.
        /// </summary>
        /// <param name="handler">The asynchronous handler.</param>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override void DataPortal_Create(Csla.DataPortalClient.LocalProxy<BatchEdit>.CompletedHandler handler)
        {
            LoadProperty(NumProperty, System.Threading.Interlocked.Decrement(ref _lastID));
            LoadProperty(DateProperty, null);
            LoadProperty(PayDateProperty, null);
            LoadProperty(DescriptionProperty, null);
            LoadProperty(UpdatedProperty, null);
            LoadProperty(ThankYou1Property, null);
            LoadProperty(StudyTopicProperty, null);
            LoadProperty(ThankYou2Property, null);
            LoadProperty(MarketingResearchMessageProperty, null);
            LoadProperty(VouchersProperty, DataPortal.CreateChild<VoucherList>());
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
