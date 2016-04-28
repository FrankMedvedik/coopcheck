using System;
using Csla;

namespace CoopCheck.DataAccess
{
    /// <summary>
    /// DTO for Account type
    /// </summary>
    public partial class AccountDto
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

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Number.
        /// </summary>
        /// <value>The Number.</value>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the Balance.
        /// </summary>
        /// <value>The Balance.</value>
        public Decimal? Balance { get; set; }

        /// <summary>
        /// Gets or sets the Last Reconciliation Date.
        /// </summary>
        /// <value>The Last Reconciliation Date.</value>
        public SmartDate LastReconciliationDate { get; set; }

        /// <summary>
        /// Gets or sets the Last Reconciliation Balance.
        /// </summary>
        /// <value>The Last Reconciliation Balance.</value>
        public Decimal? LastReconciliationBalance { get; set; }
    }
}
