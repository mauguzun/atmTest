using System.Collections.Generic;

namespace Data.Models
{
    public struct Money
    {
        /// <summary>
        /// Amount
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Amount in different paper Notes
        /// </summary>
        public Dictionary<PaperNote, int> Notes { get; set; } 
    }
}