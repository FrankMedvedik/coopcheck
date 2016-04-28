using System;

namespace CoopCheck.WPF.Interfaces
{
    public interface ICheckInfo
    {
        /// <summary>
        /// Gets the Id.
        /// </summary>
        /// <value>The Id.</value>
        int Id { get; }

        /// <summary>
        /// Gets the Date.
        /// </summary>
        /// <value>The Date.</value>
        string Date { get; }

        /// <summary>
        /// Gets the Batch Num.
        /// </summary>
        /// <value>The Batch Num.</value>
        int? BatchNum { get; }

        /// <summary>
        /// Gets the Account Id.
        /// </summary>
        /// <value>The Account Id.</value>
        int? AccountId { get; }

        /// <summary>
        /// Gets the Check Num.
        /// </summary>
        /// <value>The Check Num.</value>
        string CheckNum { get; }

        /// <summary>
        /// Gets the Amount.
        /// </summary>
        /// <value>The Amount.</value>
        Decimal? Amount { get; }

        /// <summary>
        /// Gets the Cleared Amount.
        /// </summary>
        /// <value>The Cleared Amount.</value>
        Decimal? ClearedAmount { get; }

        /// <summary>
        /// Gets the Cleared Date.
        /// </summary>
        /// <value>The Cleared Date.</value>
        string ClearedDate { get; }

        /// <summary>
        /// Gets the Is Printed.
        /// </summary>
        /// <value><c>true</c> if Is Printed; otherwise, <c>false</c>.</value>
        bool IsPrinted { get; }

        /// <summary>
        /// Gets the Is Cleared.
        /// </summary>
        /// <value><c>true</c> if Is Cleared; otherwise, <c>false</c>.</value>
        bool IsCleared { get; }

        /// <summary>
        /// Gets the Person Id.
        /// </summary>
        /// <value>The Person Id.</value>
        string PersonId { get; }

        /// <summary>
        /// Gets the Prefix.
        /// </summary>
        /// <value>The Prefix.</value>
        string Prefix { get; }

        /// <summary>
        /// Gets the First.
        /// </summary>
        /// <value>The First.</value>
        string First { get; }

        /// <summary>
        /// Gets the Middle.
        /// </summary>
        /// <value>The Middle.</value>
        string Middle { get; }

        /// <summary>
        /// Gets the Last.
        /// </summary>
        /// <value>The Last.</value>
        string Last { get; }

        /// <summary>
        /// Gets the Suffix.
        /// </summary>
        /// <value>The Suffix.</value>
        string Suffix { get; }

        /// <summary>
        /// Gets the Title.
        /// </summary>
        /// <value>The Title.</value>
        string Title { get; }

        /// <summary>
        /// Gets the Company.
        /// </summary>
        /// <value>The Company.</value>
        string Company { get; }

        /// <summary>
        /// Gets the Address Line1.
        /// </summary>
        /// <value>The Address Line1.</value>
        string AddressLine1 { get; }

        /// <summary>
        /// Gets the Address Line2.
        /// </summary>
        /// <value>The Address Line2.</value>
        string AddressLine2 { get; }

        /// <summary>
        /// Gets the Municipality.
        /// </summary>
        /// <value>The Municipality.</value>
        string Municipality { get; }

        /// <summary>
        /// Gets the Region.
        /// </summary>
        /// <value>The Region.</value>
        string Region { get; }

        /// <summary>
        /// Gets the Postal Code.
        /// </summary>
        /// <value>The Postal Code.</value>
        string PostalCode { get; }

        /// <summary>
        /// Gets the Country.
        /// </summary>
        /// <value>The Country.</value>
        string Country { get; }

        /// <summary>
        /// Gets the Phone Number.
        /// </summary>
        /// <value>The Phone Number.</value>
        string PhoneNumber { get; }

        /// <summary>
        /// Gets the Email.
        /// </summary>
        /// <value>The Email.</value>
        string Email { get; }

        /// <summary>
        /// Gets the Updated.
        /// </summary>
        /// <value>The Updated.</value>
        string Updated { get; }
    }
}