using System;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
#if SILVERLIGHT
using Csla.Serialization;
#else
using CoopCheck.DataAccess;
#endif

namespace CoopCheck.Library
{

    /// <summary>
    /// Batch (editable root object).<br/>
    /// This is a generated base class of <see cref="Batch"/> business object.
    /// </summary>
    /// <remarks>
    /// This class contains one child collection:<br/>
    /// - <see cref="VoucherList"/> of type <see cref="VoucherList"/> (1:M relation to <see cref="Voucher"/>)
    /// </remarks>
    [Serializable]
    public partial class Batch : BusinessBase<Batch>
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
        /// Maintains metadata about <see cref="BatchDate"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> BatchDateProperty = RegisterProperty<SmartDate>(p => p.BatchDate, "Batch Date");
        /// <summary>
        /// Gets or sets the Batch Date.
        /// </summary>
        /// <value>The Batch Date.</value>
        public string BatchDate
        {
            get { return GetPropertyConvert<SmartDate, String>(BatchDateProperty); }
            set { SetPropertyConvert<SmartDate, String>(BatchDateProperty, value); }
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
        /// Maintains metadata about <see cref="ThankYouMessage1"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> ThankYouMessage1Property = RegisterProperty<string>(p => p.ThankYouMessage1, "Thank You Message1");
        /// <summary>
        /// Gets or sets the Thank You Message1.
        /// </summary>
        /// <value>The Thank You Message1.</value>
        public string ThankYouMessage1
        {
            get { return GetProperty(ThankYouMessage1Property); }
            set { SetProperty(ThankYouMessage1Property, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="StudyTopic"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> StudyTopicProperty = RegisterProperty<string>(p => p.StudyTopic, "Study Topic");
        /// <summary>
        /// Gets or sets the Study Topic.
        /// </summary>
        /// <value>The Study Topic.</value>
        public string StudyTopic
        {
            get { return GetProperty(StudyTopicProperty); }
            set { SetProperty(StudyTopicProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="ThankYouMessage2"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> ThankYouMessage2Property = RegisterProperty<string>(p => p.ThankYouMessage2, "Thank You Message2");
        /// <summary>
        /// Gets or sets the Thank You Message2.
        /// </summary>
        /// <value>The Thank You Message2.</value>
        public string ThankYouMessage2
        {
            get { return GetProperty(ThankYouMessage2Property); }
            set { SetProperty(ThankYouMessage2Property, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MarketingResearchMessage"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> MarketingResearchMessageProperty = RegisterProperty<string>(p => p.MarketingResearchMessage, "Marketing Research Message");
        /// <summary>
        /// Gets or sets the Marketing Research Message.
        /// </summary>
        /// <value>The Marketing Research Message.</value>
        public string MarketingResearchMessage
        {
            get { return GetProperty(MarketingResearchMessageProperty); }
            set { SetProperty(MarketingResearchMessageProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about child <see cref="VoucherList"/> property.
        /// </summary>
        public static readonly PropertyInfo<Voucher> VoucherListProperty = RegisterProperty<Voucher>(p => p.VoucherList, "Voucher List", RelationshipTypes.Child);
        /// <summary>
        /// Gets the Voucher List ("parent load" child property).
        /// </summary>
        /// <value>The Voucher List.</value>
        public Voucher VoucherList
        {
            get { return GetProperty(VoucherListProperty); }
            private set { LoadProperty(VoucherListProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about child <see cref="VoucherList"/> property.
        /// </summary>
        public static readonly PropertyInfo<VoucherList> VoucherListProperty = RegisterProperty<VoucherList>(p => p.VoucherList, "Voucher List", RelationshipTypes.Child);
        /// <summary>
        /// Gets the Voucher List ("parent load" child property).
        /// </summary>
        /// <value>The Voucher List.</value>
        public VoucherList VoucherList
        {
            get { return GetProperty(VoucherListProperty); }
            set { SetProperty(VoucherListProperty, value); }
        }

        #endregion

        #region Factory Methods

#if !SILVERLIGHT

        /// <summary>
        /// Factory method. Creates a new <see cref="Batch"/> object.
        /// </summary>
        /// <returns>A reference to the created <see cref="Batch"/> object.</returns>
        public static Batch NewBatch()
        {
            return DataPortal.Create<Batch>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="Batch"/> object, based on given parameters.
        /// </summary>
        /// <param name="num">The Num parameter of the Batch to fetch.</param>
        /// <returns>A reference to the fetched <see cref="Batch"/> object.</returns>
        public static Batch GetBatch(int num)
        {
            return DataPortal.Fetch<Batch>(num);
        }

        /// <summary>
        /// Factory method. Deletes a <see cref="Batch"/> object, based on given parameters.
        /// </summary>
        /// <param name="num">The Num of the Batch to delete.</param>
        public static void DeleteBatch(int num)
        {
            DataPortal.Delete<Batch>(num);
        }

#endif

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="Batch"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewBatch(EventHandler<DataPortalResult<Batch>> callback)
        {
            DataPortal.BeginCreate<Batch>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="Batch"/> object, based on given parameters.
        /// </summary>
        /// <param name="num">The Num parameter of the Batch to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBatch(int num, EventHandler<DataPortalResult<Batch>> callback)
        {
            DataPortal.BeginFetch<Batch>(num, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously deletes a <see cref="Batch"/> object, based on given parameters.
        /// </summary>
        /// <param name="num">The Num of the Batch to delete.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void DeleteBatch(int num, EventHandler<DataPortalResult<Batch>> callback)
        {
            DataPortal.BeginDelete<Batch>(num, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Batch"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public Batch()
#else
        private Batch()
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
            BusinessRules.AddRule(typeof (Batch), new IsInRole(AuthorizationActions.CreateObject, "RECKNER\\CoopCheckWriter"));
            BusinessRules.AddRule(typeof (Batch), new IsInRole(AuthorizationActions.GetObject, "RECKNER\\CoopCheckReader"));
            BusinessRules.AddRule(typeof (Batch), new IsInRole(AuthorizationActions.EditObject, "RECKNER\\CoopCheckWriter"));
            BusinessRules.AddRule(typeof (Batch), new IsInRole(AuthorizationActions.DeleteObject, "RECKNER\\CoopCheckWriter"));

            AddObjectAuthorizationRulesExtend();
        }

        /// <summary>
        /// Allows the set up of custom object authorization rules.
        /// </summary>
        static partial void AddObjectAuthorizationRulesExtend();

        /// <summary>
        /// Checks if the current user can create a new Batch object.
        /// </summary>
        /// <returns><c>true</c> if the user can create a new object; otherwise, <c>false</c>.</returns>
        public static bool CanAddObject()
        {
        #if DEBUG
            return true;
	#endif
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.CreateObject, typeof(Batch));
        }

        /// <summary>
        /// Checks if the current user can retrieve Batch's properties.
        /// </summary>
        /// <returns><c>true</c> if the user can read the object; otherwise, <c>false</c>.</returns>
        public static bool CanGetObject()
        {
        #if DEBUG
	            return true;
	#endif
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.GetObject, typeof(Batch));
        }

        /// <summary>
        /// Checks if the current user can change Batch's properties.
        /// </summary>
        /// <returns><c>true</c> if the user can update the object; otherwise, <c>false</c>.</returns>
        public static bool CanEditObject()
        {
        #if DEBUG
	            return true;
	#endif
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.EditObject, typeof(Batch));
        }

        /// <summary>
        /// Checks if the current user can delete a Batch object.
        /// </summary>
        /// <returns><c>true</c> if the user can delete the object; otherwise, <c>false</c>.</returns>
        public static bool CanDeleteObject()
        {
	#if DEBUG
		    return true;
	#endif
	return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.DeleteObject, typeof(Batch));
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="Batch"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void DataPortal_Create()
        {
            LoadProperty(NumProperty, System.Threading.Interlocked.Decrement(ref _lastID));
            LoadProperty(BatchDateProperty, null);
            LoadProperty(PayDateProperty, null);
            LoadProperty(DescriptionProperty, null);
            LoadProperty(UpdatedProperty, null);
            LoadProperty(ThankYouMessage1Property, null);
            LoadProperty(StudyTopicProperty, null);
            LoadProperty(ThankYouMessage2Property, null);
            LoadProperty(MarketingResearchMessageProperty, null);
            LoadProperty(VoucherListProperty, DataPortal.CreateChild<Voucher>());
            LoadProperty(VoucherListProperty, DataPortal.CreateChild<VoucherList>());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.DataPortal_Create();
        }

#if !SILVERLIGHT

        /// <summary>
        /// Loads a <see cref="Batch"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="num">The Num.</param>
        protected void DataPortal_Fetch(int num)
        {
            var args = new DataPortalHookArgs(num);
            OnFetchPre(args);
            using (var dalManager = DalFactoryGetManager())
            {
                var dal = dalManager.GetProvider<IBatchDal>();
                var data = dal.Fetch(num);
                Fetch(data);
                FetchChildren(dal);
            }
            OnFetchPost(args);
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        /// <summary>
        /// Loads a <see cref="Batch"/> object from the given <see cref="BatchDto"/>.
        /// </summary>
        /// <param name="data">The BatchDto to use.</param>
        private void Fetch(BatchDto data)
        {
            // Value properties
            LoadProperty(NumProperty, data.Num);
            LoadProperty(BatchDateProperty, data.BatchDate);
            LoadProperty(PayDateProperty, data.PayDate);
            LoadProperty(AmountProperty, data.Amount);
            LoadProperty(JobNumProperty, data.JobNum);
            LoadProperty(DescriptionProperty, data.Description);
            LoadProperty(UpdatedProperty, data.Updated);
            LoadProperty(ThankYouMessage1Property, data.ThankYouMessage1);
            LoadProperty(StudyTopicProperty, data.StudyTopic);
            LoadProperty(ThankYouMessage2Property, data.ThankYouMessage2);
            LoadProperty(MarketingResearchMessageProperty, data.MarketingResearchMessage);
            var args = new DataPortalHookArgs(data);
            OnFetchRead(args);
        }

        /// <summary>
        /// Loads child objects from the given DAL provider.
        /// </summary>
        /// <param name="dal">The DAL provider to use.</param>
        private void FetchChildren(IBatchDal dal)
        {
            LoadProperty(VoucherListProperty, Voucher.GetVoucher(dal.Voucher));
            LoadProperty(VoucherListProperty, VoucherList.GetVoucherList(dal.VoucherList));
        }

        /// <summary>
        /// Inserts a new <see cref="Batch"/> object in the database.
        /// </summary>
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            var dto = new BatchDto();
            dto.BatchDate = ReadProperty(BatchDateProperty);
            dto.PayDate = ReadProperty(PayDateProperty);
            dto.Amount = Amount;
            dto.JobNum = JobNum;
            dto.Description = Description;
            dto.Updated = ReadProperty(UpdatedProperty);
            dto.ThankYouMessage1 = ThankYouMessage1;
            dto.StudyTopic = StudyTopic;
            dto.ThankYouMessage2 = ThankYouMessage2;
            dto.MarketingResearchMessage = MarketingResearchMessage;
            using (var dalManager = DalFactoryGetManager())
            {
                var args = new DataPortalHookArgs(dto);
                OnInsertPre(args);
                var dal = dalManager.GetProvider<IBatchDal>();
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
        /// Updates in the database all changes made to the <see cref="Batch"/> object.
        /// </summary>
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            var dto = new BatchDto();
            dto.Num = Num;
            dto.BatchDate = ReadProperty(BatchDateProperty);
            dto.PayDate = ReadProperty(PayDateProperty);
            dto.Amount = Amount;
            dto.JobNum = JobNum;
            dto.Description = Description;
            dto.Updated = ReadProperty(UpdatedProperty);
            dto.ThankYouMessage1 = ThankYouMessage1;
            dto.StudyTopic = StudyTopic;
            dto.ThankYouMessage2 = ThankYouMessage2;
            dto.MarketingResearchMessage = MarketingResearchMessage;
            using (var dalManager = DalFactoryGetManager())
            {
                var args = new DataPortalHookArgs(dto);
                OnUpdatePre(args);
                var dal = dalManager.GetProvider<IBatchDal>();
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
        /// Self deletes the <see cref="Batch"/> object.
        /// </summary>
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(Num);
        }

        /// <summary>
        /// Deletes the <see cref="Batch"/> object from database.
        /// </summary>
        /// <param name="num">The delete criteria.</param>
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(int num)
        {
            using (var dalManager = DalFactoryGetManager())
            {
                var args = new DataPortalHookArgs();
                // flushes all pending data operations
                FieldManager.UpdateChildren(this);
                OnDeletePre(args);
                var dal = dalManager.GetProvider<IBatchDal>();
                using (BypassPropertyChecks)
                {
                    dal.Delete(num);
                }
                OnDeletePost(args);
            }
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
