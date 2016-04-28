using System;
using Csla;

namespace CoopCheck.DataAccess
{
    /// <summary>
    /// DTO for Voucher type
    /// </summary>
    public partial class VoucherDto
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
        /// Gets or sets the First Name.
        /// </summary>
        /// <value>The First Name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the Middle Name.
        /// </summary>
        /// <value>The Middle Name.</value>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the Last Name.
        /// </summary>
        /// <value>The Last Name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the Name Suffix.
        /// </summary>
        /// <value>The Name Suffix.</value>
        public string NameSuffix { get; set; }

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
