using System;
using System.ComponentModel.DataAnnotations;

namespace CoopCheck.Library.Interfaces
{
    public interface IVoucherEdit
    {
        /// <summary>
        /// Gets the Id.
        /// </summary>
        /// <value>The Id.</value>
        int Id { get; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        /// <value>The Amount.</value>
        [Display(Name = "Voucher Amount"), Range(-500, 5000)]
        Decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the Person Id.
        /// </summary>
        /// <value>The Person Id.</value>
        [StringLength(50)]
        string PersonId { get; set; }

        /// <summary>
        /// Gets or sets the Name Prefix.
        /// </summary>
        /// <value>The Name Prefix.</value>
        [StringLength(5)]
        string NamePrefix { get; set; }

        /// <summary>
        /// Gets or sets the First.
        /// </summary>
        /// <value>The First.</value>
        [Display(Name = "First Name"), StringLength(20)]
        string First { get; set; }

        /// <summary>
        /// Gets or sets the Middle.
        /// </summary>
        /// <value>The Middle.</value>
        [Display(Name = "Middle Name"), StringLength(20)]
        string Middle { get; set; }

        /// <summary>
        /// Gets or sets the Last.
        /// </summary>
        /// <value>The Last.</value>
        [Display(Name = "Last Name"), StringLength(20)]
        string Last { get; set; }

        /// <summary>
        /// Gets or sets the Suffix.
        /// </summary>
        /// <value>The Suffix.</value>
        [Display(Name = "Name Suffix"), StringLength(15)]
        string Suffix { get; set; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        /// <value>The Title.</value>
        [StringLength(35)]
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the Company.
        /// </summary>
        /// <value>The Company.</value>
        [StringLength(35)]
        string Company { get; set; }

        /// <summary>
        /// Gets or sets the Address Line1.
        /// </summary>
        /// <value>The Address Line1.</value>
        [StringLength(50)]
        [Required]
        string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the Address Line2.
        /// </summary>
        /// <value>The Address Line2.</value>
        [StringLength(50)]
        string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the Municipality.
        /// </summary>
        /// <value>The Municipality.</value>
        [StringLength(35)]
        [Required]
        string Municipality { get; set; }

        /// <summary>
        /// Gets or sets the Region.
        /// </summary>
        /// <value>The Region.</value>
        [StringLength(35)]
        [Required]
        string Region { get; set; }

        /// <summary>
        /// Gets or sets the Postal Code.
        /// </summary>
        /// <value>The Postal Code.</value>
        [StringLength(15)]
        [DataType(DataType.PostalCode)]
        string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        /// <value>The Country.</value>
        [StringLength(2)]
        string Country { get; set; }

        /// <summary>
        /// Gets or sets the Phone Number.
        /// </summary>
        /// <value>The Phone Number.</value>
        [StringLength(22)]
        [DataType(DataType.PhoneNumber)]
        string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Email Address.
        /// </summary>
        /// <value>The Email Address.</value>
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the Updated.
        /// </summary>
        /// <value>The Updated.</value>
        string Updated { get; set; }
    }
}