using System;
using CoopCheck.Library.Contracts.Interfaces;
using Csla;
using Csla.Rules;
#if SILVERLIGHT
using Csla.Serialization;
#else
using System.Collections.Generic;
using CoopCheck.DAL;
#endif

namespace CoopCheck.Library
{
    [Serializable]
#if WINFORMS
    public class PaymentInfoList : ReadOnlyBindingListBase<PaymentInfoList, PaymentInfo>
#else
    public class PaymentInfoList : ReadOnlyListBase<PaymentInfoList, IPaymentInfo>, IPaymentInfoList
#endif
    {
#region Methods
        public bool Contains(int id)
        {
            foreach (var info in this)
            {
                if (info.Id == id)
                {
                    return true;
                }
            }
            return false;
        }
#endregion
        #region Factory Methods
#if !SILVERLIGHT
        public static PaymentInfoList GetPaymentInfoListByBatch(int batchNum)
        {
            return DataPortal.Fetch<PaymentInfoList>(new BatchCriteria() { Num = batchNum });
        }
        public static PaymentInfoList GetPaymentInfoListByJob(int jobNum)
        {
            return DataPortal.Fetch<PaymentInfoList>(new JobCriteria() { Num = jobNum });
        }
        public static PaymentInfoList GetPaymentInfoListByPersonId(string personId)
        {
            return DataPortal.Fetch<PaymentInfoList>(personId);
        }
        internal static PaymentInfoList LoadPaymentInfoList(List<PaymentInfoDto> data)
        {
            var l = new PaymentInfoList();
            l.Fetch(data);
            return l;
        }
#endif
        public static void GetPaymentInfoListByBatch(int batchNum, EventHandler<DataPortalResult<PaymentInfoList>> callback)
        {
            DataPortal.BeginFetch<PaymentInfoList>(batchNum, callback);
        }
        #endregion

        #region Constructor

#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public PaymentInfoList()
#else
        private PaymentInfoList()
#endif
        {
            var rlce = RaiseListChangedEvents;
            AllowNew = false;
            AllowEdit = false;
            AllowRemove = false;
            RaiseListChangedEvents = rlce;
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
#if !DEBUG
            //BusinessRules.AddRule(typeof(Account), new IsInRole(AuthorizationActions.GetObject, "RECKNER\\CoopCheckReader"));
            BusinessRules.AddRule(typeof(PaymentInfoList), new IsInRole(AuthorizationActions.GetObject, "RECKNER\\CoopCheckReader"));
#endif

        }


        /// <summary>
        /// Checks if the current user can retrieve Account's properties.
        /// </summary>
        /// <returns><c>true</c> if the user can read the object; otherwise, <c>false</c>.</returns>
        public static bool CanGetObject()
        {
            return BusinessRules.HasPermission(Csla.Rules.AuthorizationActions.GetObject, typeof(Account));
        }

        
        #endregion
#region Data Access
        #region CriteriaClass(es)
        [Serializable()]
        protected class BatchCriteria : CriteriaBase<BatchCriteria>
        {
            public static readonly PropertyInfo<int> NumProperty = RegisterProperty<int>(c => c.Num);
            public int Num
            {
                get { return ReadProperty(NumProperty); }
                set { LoadProperty(NumProperty, value); }
            }
        }
        [Serializable()]
        protected class JobCriteria : CriteriaBase<JobCriteria>
        {
            public static readonly PropertyInfo<int> NumProperty = RegisterProperty<int>(c => c.Num);
            public int Num  
            {
                get { return ReadProperty(NumProperty); }
                set { LoadProperty(NumProperty, value); }
            }
        }
        [Serializable()]
        protected class IdCriteria : CriteriaBase<IdCriteria>
        {
            public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
            public int Id
            {
                get { return ReadProperty(IdProperty); }
                set { LoadProperty(IdProperty, value); }
            }
        }
        [Serializable()]
        protected class NumCriteria : CriteriaBase<NumCriteria>
        {
            public static readonly PropertyInfo<string> NumProperty = RegisterProperty<string>(c => c.Num);
            public string Num
            {
                get { return ReadProperty(NumProperty); }
                set { LoadProperty(NumProperty, value); }
            }
        }




        #endregion

#if !SILVERLIGHT
        protected void DataPortal_Fetch(BatchCriteria crit)
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                var data = dal.FetchBatch(crit.Num);
                Fetch(data);
            }
        }
        protected void DataPortal_Fetch(JobCriteria crit)
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                var data = dal.FetchJob(crit.Num);
                Fetch(data);
            }
        }
        protected void DataPortal_Fetch(string personId)
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                var data = dal.FetchPerson(personId);
                Fetch(data);
            }
        }
        protected void DataPortal_Fetch(IdCriteria crit)
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                var data = dal.FetchById(crit.Id);
                Fetch(data);
            }
        }
        protected void DataPortal_Fetch(NumCriteria crit)
        {
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IPaymentInfoListDal>();
                var data = dal.FetchByNum(crit.Num);
                Fetch(data);
            }
        }
        private void Fetch(List<PaymentInfoDto> data)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            foreach (var dto in data){
                Add(PaymentInfo.GetPaymentInfo(dto));
            }
            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }
#endif
#endregion
    }

}
