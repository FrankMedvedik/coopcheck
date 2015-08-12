using System;
using Csla;

namespace CoopCheck.DAL.Library
{
    /// <summary>
    /// Item DTO for AccountList type
    /// </summary>
    public partial class AccountListItemDto
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>The Id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name { get; set; }
    }
}
