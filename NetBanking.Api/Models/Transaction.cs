using System.ComponentModel.DataAnnotations;

namespace NetBanking.Api.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        public string Description { get; set; }

        // Foreign key for the source account (the account the money is coming from)
        public int SourceAccountId { get; set; }
        public Account SourceAccount { get; set; }

        // Foreign key for the destination account (the account the money is going to)
        public int DestinationAccountId { get; set; }
        public Account DestinationAccount { get; set; }
    }
}