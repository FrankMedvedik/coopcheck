using System;

namespace CoopCheck.Library.Contracts.Interfaces
{
    public interface IAccount
    {
        /// <summary>
        /// Gets the Id.
        /// </summary>
        /// <value>The Id.</value>
        int Id { get; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>The Name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the Number.
        /// </summary>
        /// <value>The Number.</value>
        string Number { get; set; }

        /// <summary>
        /// Gets or sets the Balance.
        /// </summary>
        /// <value>The Balance.</value>
        Decimal? Balance { get; set; }

        /// <summary>
        /// Gets or sets the Last Reconciliation Date.
        /// </summary>
        /// <value>The Last Reconciliation Date.</value>
        string LastReconciliationDate { get; set; }

        /// <summary>
        /// Gets or sets the Last Reconciliation Balance.
        /// </summary>
        /// <value>The Last Reconciliation Balance.</value>
        Decimal? LastReconciliationBalance { get; set; }
    }
}