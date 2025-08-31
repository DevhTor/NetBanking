using NetBanking.Api.Models;

namespace NetBanking.Api.Services
{
    public interface IAuthService
    {
        Task<Client> AuthenticateAsync(string username, string password);
        string GenerateJwtToken(Client client);
    }
}