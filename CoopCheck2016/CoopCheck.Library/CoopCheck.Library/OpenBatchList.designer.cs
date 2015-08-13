using System;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
#if SILVERLIGHT
using Csla.Serialization;
#else
using System.Collections.Generic;
using CoopCheck.DAL;
#endif

namespace CoopCheck.Library
{

    /// <summary>
    /// OpenBatchList (name value list).<br/>
    /// This is a generated base class of <see cref="OpenBatchList"/> business object.
    /// </summary>
    [Serializable]
    public partial class OpenBatchList : NameValueListBase<int, string>
    {

        #region Factory Methods

#if !SILVERLIGHT

        /// <summary>
        /// Factory method. Loads a <see cref="OpenBatchList"/> object.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="OpenBatchList"/> object.</returns>
        public static OpenBatchList GetOpenBatchList()
        {
            return DataPortal.Fetch<OpenBatchList>();
        }

#else

#endif

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="OpenBatchList"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetOpenBatchList(EventHandler<DataPortalResult<OpenBatchList>> callback)
        {
            DataPortal.BeginFetch<OpenBatchList>(callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenBatchList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public OpenBatchList()
#else
        private OpenBatchList()
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
            
            BusinessRules.AddRule(typeof(Account), new IsInRole(AuthorizationActions.GetObject, "RECKNER\\CoopCheckReader"));
            
            AddObjectAuthorizationRulesExtend();
        }

        /// <summary>
        /// Allows the set up of custom object authorization rules.
        /// </summary>
        static partial void AddObjectAuthorizationRulesExtend();

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

#if !SILVERLIGHT

        /// <summary>
        /// Loads a <see cref="OpenBatchList"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            var args = new DataPortalHookArgs();
            OnFetchPre(args);
            using (var dalManager = DalFactory.GetManager())
            {
                var dal = dalManager.GetProvider<IOpenBatchListDal>();
                var data = dal.Fetch();
                LoadCollection(data);
            }
            OnFetchPost(args);
        }

        private void LoadCollection(List<OpenBatchListItemDto> data)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            foreach (var dto in data)
            {
                Add(new NameValuePair(dto.Num, dto.Label));
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
