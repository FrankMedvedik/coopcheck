using System;
using Csla;
#if SILVERLIGHT
using Csla.Serialization;
#else
using System.Collections.Generic;
using CoopCheck.DAL;
#endif

namespace CoopCheck.Library
{

    /// <summary>
    /// BatchList (name value list).<br/>
    /// This is a generated base class of <see cref="BatchList"/> business object.
    /// </summary>
    [Serializable]
    public partial class BatchList : NameValueListBase<int, string>
    {

        #region Factory Methods

#if !SILVERLIGHT

        /// <summary>
        /// Factory method. Loads a <see cref="BatchList"/> object.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="BatchList"/> object.</returns>
        public static BatchList GetBatchList()
        {
            return DataPortal.Fetch<BatchList>();
        }

#else

#endif

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BatchList"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBatchList(EventHandler<DataPortalResult<BatchList>> callback)
        {
            DataPortal.BeginFetch<BatchList>(callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public BatchList()
#else
        private BatchList()
#endif
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

#if !SILVERLIGHT

        /// <summary>
        /// Loads a <see cref="BatchList"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            var args = new DataPortalHookArgs();
            OnFetchPre(args);
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IBatchListDal>();
                var data = dal.Fetch();
                LoadCollection(data);
            }
            OnFetchPost(args);
        }

        private void LoadCollection(List<BatchListItemDto> data)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            foreach (var dto in data)
            {
                Add(new NameValuePair(dto.Batch_num, dto.Batch_dscr));
            }
            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

#endif

        #endregion

        #region Pseudo Events

#if !SILVERLIGHT

        /// <summary>
        /// Occurs after setting query parameters and before the fetch operation.
        /// </summary>
        partial void OnFetchPre(DataPortalHookArgs args);

        /// <summary>
        /// Occurs after the fetch operation (object or collection is fully loaded and set up).
        /// </summary>
        partial void OnFetchPost(DataPortalHookArgs args);

#endif

        #endregion

    }
}
