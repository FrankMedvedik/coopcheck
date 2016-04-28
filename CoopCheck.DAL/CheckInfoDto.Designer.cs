using System;
using Csla;

namespace CoopCheck.DAL
{
    /// <summary>
    /// DTO for CheckInfo type
    /// </summary>
    /// 
    [Serializable]
    public partial class CheckInfoDto
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>The Id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Date.
        /// </summary>
        /// <value>The Date.</value>
        public SmartDate Date { get; set; }

        /// <summary>
        /// Gets or sets the Batch Num.
        /// </summary>
        /// <value>The Batch Num.</value>
        public int? BatchNum { get; set; }

        /// <summary>
        /// Gets or sets the Account Id.
        /// </summary>
        /// <value>The Account Id.</value>
        public int? AccountId { get; set; }

        /// <summary>
        /// Gets or sets the Check Num.
        /// </summary>
        /// <value>The Check Num.</value>
        public string CheckNum { get; set; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        /// <value>The Amount.</value>
        public Decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the Cleared Amount.
        /// </summary>
        /// <value>The Cleared Amount.</value>
        public Decimal? ClearedAmount { get; set; }

        /// <summary>
        /// Gets or sets the Cleared Date.
        /// </summary>
        /// <value>The Cleared Date.</value>
        public SmartDate ClearedDate { get; set; }

        /// <summary>
        /// Gets or sets the Is Printed.
        /// </summary>
        /// <value><c>true</c> if Is Printed; otherwise, <c>false</c>.</value>
        public bool IsPrinted { get; set; }

        /// <summary>
        /// Gets or sets the Is Cleared.
        /// </summary>
        /// <value><c>true</c> if Is Cleared; otherwise, <c>false</c>.</value>
        public bool IsCleared { get; set; }

        /// <summary>
        /// Gets or sets the Person Id.
        /// </summary>
        /// <value>The Person Id.</value>
        public string PersonId { get; set; }

        /// <summary>
        /// Gets or sets the Prefix.
        /// </summary>
        /// <value>The Prefix.</value>
        public string Prefix { get; set; }

        /// <summary>
        /// Gets or sets the First.
        /// </summary>
        /// <value>The First.</value>
        public string First { get; set; }

        /// <summary>
        /// Gets or sets the Middle.
        /// </summary>
        /// <value>The Middle.</value>
        public string Middle { get; set; }

        /// <summary>
        /// Gets or sets the Last.
        /// </summary>
        /// <value>The Last.</value>
        public string Last { get; set; }

        /// <summary>
        /// Gets or sets the Suffix.
        /// </summary>
        /// <value>The Suffix.</value>
        public string Suffix { get; set; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        /// <value>The Title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Company.
        /// </summary>
        /// <value>The Company.</value>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the Address Line1.
        /// </summary>
        /// <value>The Address Line1.</value>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the Address Line2.
        /// </summary>
        /// <value>The Address Line2.</value>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the Municipality.
        /// </summary>
        /// <value>The Municipality.</value>
        public string Municipality { get; set; }

        /// <summary>
        /// Gets or sets the Region.
        /// </summary>
        /// <value>The Region.</value>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the Postal Code.
        /// </summary>
        /// <value>The Postal Code.</value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        /// <value>The Country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the Phone Number.
        /// </summary>
        /// <value>The Phone Number.</value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        /// <value>The Email.</value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Updated.
        /// </summary>
        /// <value>The Updated.</value>
        public SmartDate Updated { get; set; }
    }
}
