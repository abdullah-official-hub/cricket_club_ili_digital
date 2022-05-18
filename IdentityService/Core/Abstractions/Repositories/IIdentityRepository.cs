using IdentityService.Core.Domain.Models;
using System.Threading.Tasks;

namespace IdentityService.Core.Abstractions.Repositories
{
    public interface IIdentityRepository
    {
        Task AddAsync(Identity identity);
        Task<Identity> GetIdentityForAuthentication(string email);
    }
}