using System;
using Csla;

namespace CoopCheck.DataAccess
{
    /// <summary>
    /// DTO for Batch type
    /// </summary>
    public partial class BatchDto
    {
        /// <summary>
        /// Gets or sets the Num.
        /// </summary>
        /// <value>The Num.</value>
        public int Num { get; set; }

        /// <summary>
        /// Gets or sets the Batch Date.
        /// </summary>
        /// <value>The Batch Date.</value>
        public SmartDate BatchDate { get; set; }

        /// <summary>
        /// Gets or sets the Pay Date.
        /// </summary>
        /// <value>The Pay Date.</value>
        public SmartDate PayDate { get; set; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        /// <value>The Amount.</value>
        public Decimal? Amount { get; set; }

        /// <summary>
        /// Gets or sets the Job Num.
        /// </summary>
        /// <value>The Job Num.</value>
        public int? JobNum { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Updated.
        /// </summary>
        /// <value>The Updated.</value>
        public SmartDate Updated { get; set; }

        /// <summary>
        /// Gets or sets the Thank You Message1.
        /// </summary>
        /// <value>The Thank You Message1.</value>
        public string ThankYouMessage1 { get; set; }

        /// <summary>
        /// Gets or sets the Study Topic.
        /// </summary>
        /// <value>The Study Topic.</value>
        public string StudyTopic { get; set; }

        /// <summary>
        /// Gets or sets the Thank You Message2.
        /// </summary>
        /// <value>The Thank You Message2.</value>
        public string ThankYouMessage2 { get; set; }

        /// <summary>
        /// Gets or sets the Marketing Research Message.
        /// </summary>
        /// <value>The Marketing Research Message.</value>
        public string MarketingResearchMessage { get; set; }
    }
}
