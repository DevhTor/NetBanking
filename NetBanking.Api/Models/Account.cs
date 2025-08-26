using System.ComponentModel.DataAnnotations;

namespace NetBanking.Api.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
