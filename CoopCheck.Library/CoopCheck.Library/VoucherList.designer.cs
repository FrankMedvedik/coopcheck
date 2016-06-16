using System;
using CoopCheck.Library.Contracts.Interfaces;
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
    /// VoucherList (editable child list).<br/>
    /// This is a generated base class of <see cref="VoucherList"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is child of <see cref="BatchEdit"/> editable root object.<br/>
    /// The items of the collection are <see cref="VoucherEdit"/> objects.
    /// </remarks>
    [Serializable]
#if WINFORMS
    public partial class VoucherList : BusinessBindingListBase<VoucherList, VoucherEdit>
#else
    public partial class VoucherList : BusinessListBase<VoucherList,VoucherEdit>
#endif
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="VoucherEdit"/> item from the collection.
        /// </summary>
        /// <param name="id">The Id of the item to be removed.</param>
        public void Remove(int id)
        {
            foreach (var voucherEdit in this)
            {
                if (voucherEdit.Id == id)
                {
                    Remove(voucherEdit);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="VoucherEdit"/> item is in the collection.
        /// </summary>
        /// <param name="id">The Id of the item to search for.</param>
        /// <returns><c>true</c> if the VoucherEdit is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(int id)
        {
            foreach (var voucherEdit in this)
            {
                if (voucherEdit.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="VoucherEdit"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="id">The Id of the item to search for.</param>
        /// <returns><c>true</c> if the VoucherEdit is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(int id)
        {
            foreach (var voucherEdit in this.DeletedList)
            {
                if (voucherEdit.Id == id)
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
        /// Factory method. Creates a new <see cref="VoucherList"/> collection.
        /// </summary>
        /// <returns>A reference to the created <see cref="VoucherList"/> collection.</returns>
        internal static VoucherList NewVoucherList()
        {
            return DataPortal.CreateChild<VoucherList>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="VoucherList"/> object from the given list of VoucherEditDto.
        /// </summary>
        /// <param name="data">The list of <see cref="VoucherEditDto"/>.</param>
        /// <returns>A reference to the fetched <see cref="VoucherList"/> object.</returns>
        internal static VoucherList GetVoucherList(List<VoucherEditDto> data)
        {
            VoucherList obj = new VoucherList();
            // show the framework that this is a child object
            obj.MarkAsChild();
            obj.Fetch(data);
            return obj;
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="VoucherList"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        internal static void NewVoucherList(EventHandler<DataPortalResult<VoucherList>> callback)
        {
            DataPortal.BeginCreate<VoucherList>(callback);
            
        }


#else

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="VoucherList"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        internal static void NewVoucherList(EventHandler<DataPortalResult<VoucherList>> callback)
        {
            DataPortal.BeginCreate<VoucherList>(callback, DataPortal.ProxyModes.LocalOnly);
        }

#endif

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VoucherList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public VoucherList()
#else
        private VoucherList()
#endif
        {
            // Prevent direct creation

            // show the framework that this is a child object
            MarkAsChild();

            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            AllowNew = true;
            AllowEdit = true;
            AllowRemove = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion

        #region Data Access

#if !SILVERLIGHT

        /// <summary>
        /// Loads all <see cref="VoucherList"/> collection items from the given list of VoucherEditDto.
        /// </summary>
        /// <param name="data">The list of <see cref="VoucherEditDto"/>.</param>
        private void Fetch(List<VoucherEditDto> data)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            var args = new DataPortalHookArgs(data);
            OnFetchPre(args);
            foreach (var dto in data)
            {
                Add(VoucherEdit.GetVoucherEdit(dto));
            }
            OnFetchPost(args);
            RaiseListChangedEvents = rlce;
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

        public bool IsReadOnly { get; }

    }
}
