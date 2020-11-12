using System;

namespace Data.Models
{
    public struct Fee
    {
        /// <summary>
        /// Card Number
        /// </summary>
        public string CardNumber { get; set; }
        /// <summary>
        /// Withdrawal Amount
        /// </summary>
        public decimal WithdrawalFeeAmount { get; set; }
        /// <summary>
        /// Withdrawal operation date
        /// </summary>
        public DateTime WithdrawalDate { get; set; }
    }
}
