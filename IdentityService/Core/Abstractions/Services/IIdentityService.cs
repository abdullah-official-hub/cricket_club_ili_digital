using IdentityService.Core.Domain.Contracts;
using IdentityService.Core.Domain.Models;
using System.Threading.Tasks;

namespace IdentityService.Core.Abstractions.Services
{
    public interface IIdentityService
    {
        Task RegisterAsync(Identity identity);
        Task<LoginResponse> AuthenticateAsync(string email, string password);
    }
}