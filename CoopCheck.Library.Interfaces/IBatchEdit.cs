using System;
using System.ComponentModel.DataAnnotations;

namespace CoopCheck.Library.Interfaces
{
    public interface IBatchEdit
    {
        /// <summary>
        /// Gets the Num.
        /// </summary>
        /// <value>The Num.</value>
        int Num { get; }

        /// <summary>
        /// Gets or sets the Date.
        /// </summary>
        /// <value>The Date.</value>
        string Date { get; set; }

        /// <summary>
        /// Gets or sets the Pay Date.
        /// </summary>
        /// <value>The Pay Date.</value>
        string PayDate { get; set; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        /// <value>The Amount.</value>
        Decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the Job Num.
        /// </summary>
        /// <value>The Job Num.</value>
        int? JobNum { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        [StringLength(255)]
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the Updated.
        /// </summary>
        /// <value>The Updated.</value>
        string Updated { get; set; }

        /// <summary>
        /// Gets or sets the Thank You1.
        /// </summary>
        /// <value>The Thank You1.</value>
        [StringLength(100)]
        string ThankYou1 { get; set; }

        /// <summary>
        /// Gets or sets the Study Topic.
        /// </summary>
        /// <value>The Study Topic.</value>
        [StringLength(150)]
        string StudyTopic { get; set; }

        /// <summary>
        /// Gets or sets the Thank You2.
        /// </summary>
        /// <value>The Thank You2.</value>
        [StringLength(150)]
        string ThankYou2 { get; set; }

        /// <summary>
        /// Gets or sets the Marketing Research Message.
        /// </summary>
        /// <value>The Marketing Research Message.</value>
        [StringLength(500)]
        string MarketingResearchMessage { get; set; }

        /// <summary>
        /// Gets the Vouchers ("parent load" child property).
        /// </summary>
        /// <value>The Vouchers.</value>
        IVoucherList Vouchers { get; }
    }
}