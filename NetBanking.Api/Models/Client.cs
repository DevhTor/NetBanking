using System.ComponentModel.DataAnnotations;

namespace NetBanking.Api.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
