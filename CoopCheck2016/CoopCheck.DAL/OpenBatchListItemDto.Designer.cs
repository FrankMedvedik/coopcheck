using System;
using Csla;

namespace CoopCheck.DAL
{
    /// <summary>
    /// Item DTO for OpenBatchList type
    /// </summary>
    public partial class OpenBatchListItemDto
    {
        /// <summary>
        /// Gets or sets the Num.
        /// </summary>
        /// <value>The Num.</value>
        public int Num { get; set; }

        /// <summary>
        /// Gets or sets the Label.
        /// </summary>
        /// <value>The Label.</value>
        public string Label { get; set; }
    }
}
