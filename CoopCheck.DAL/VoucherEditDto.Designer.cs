using System;
using Csla;

namespace CoopCheck.DAL
{
    /// <summary>
    /// DTO for VoucherEdit type
    /// </summary>
    public partial class VoucherEditDto
    {
        /// <summary>
        /// Gets or sets the parent Num.
        /// </summary>
        /// <value>The Num.</value>
        public int Parent_Num { get; set; }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>The Id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        /// <value>The Amount.</value>
        public Decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the Person Id.
        /// </summary>
        /// <value>The Person Id.</value>
        public string PersonId { get; set; }

        /// <summary>
        /// Gets or sets the Name Prefix.
        /// </summary>
        /// <value>The Name Prefix.</value>
        public string NamePrefix { get; set; }

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
        /// Gets or sets the Email Address.
        /// </summary>
        /// <value>The Email Address.</value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the Updated.
        /// </summary>
        /// <value>The Updated.</value>
        public SmartDate Updated { get; set; }
    }
}
