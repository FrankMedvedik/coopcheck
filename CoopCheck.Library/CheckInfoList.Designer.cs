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
    /// CheckInfoList (read only list).<br/>
    /// This is a generated base class of <see cref="CheckInfoList"/> business object.
    /// This class is a root collection.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="CheckInfo"/> objects.
    /// </remarks>
    [Serializable]
#if WINFORMS
    public partial class CheckInfoList : ReadOnlyBindingListBase<CheckInfoList, CheckInfo>
#else
    public partial class CheckInfoList : ReadOnlyListBase<CheckInfoList, CheckInfo>
#endif
    {

        #region Collection Business Methods

        /// <summary>
        /// Determines whether a <see cref="CheckInfo"/> item is in the collection.
        /// </summary>
        /// <param name="id">The Id of the item to search for.</param>
        /// <returns><c>true</c> if the CheckInfo is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(int id)
        {
            foreach (var checkInfo in this)
            {
                if (checkInfo.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Factory Methods

#if !SILVERLIGHT

        /// <summary>
        /// Factory method. Loads a <see cref="CheckInfoList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="personId">The PersonId parameter of the CheckInfoList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="CheckInfoList"/> collection.</returns>
        public static CheckInfoList GetCheckInfoList(string personId)
        {
            return DataPortal.Fetch<CheckInfoList>(personId);
        }

#endif

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="CheckInfoList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="personId">The PersonId parameter of the CheckInfoList to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetCheckInfoList(string personId, EventHandler<DataPortalResult<CheckInfoList>> callback)
        {
            DataPortal.BeginFetch<CheckInfoList>(personId, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckInfoList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public CheckInfoList()
#else
        private CheckInfoList()
#endif
        {
            // Prevent direct creation

            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            AllowNew = false;
            AllowEdit = false;
            AllowRemove = false;
            RaiseListChangedEvents = rlce;
        }

        #endregion

        #region Data Access

#if !SILVERLIGHT

        /// <summary>
        /// Loads a <see cref="CheckInfoList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="personId">The Person Id.</param>
        protected void DataPortal_Fetch(string personId)
        {
            var args = new DataPortalHookArgs(personId);
            OnFetchPre(args);
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<ICheckInfoListDal>();
                var data = dal.Fetch(personId);
                Fetch(data);
            }
            OnFetchPost(args);
        }

        /// <summary>
        /// Loads all <see cref="CheckInfoList"/> collection items from the given list of CheckInfoDto.
        /// </summary>
        /// <param name="data">The list of <see cref="CheckInfoDto"/>.</param>
        private void Fetch(List<CheckInfoDto> data)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            foreach (var dto in data)
            {
                Add(CheckInfo.GetCheckInfo(dto));
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