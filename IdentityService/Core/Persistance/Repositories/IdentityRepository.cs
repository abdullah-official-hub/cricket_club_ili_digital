using IdentityService.Core.Abstractions.Repositories;
using IdentityService.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.Core.Persistance.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IdentityDbContext context;
        public IdentityRepository(IdentityDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Identity identity)
        {
            await context.Identity.AddAsync(identity);
        }

        public async Task<Identity> GetIdentityForAuthentication(string email)
        {
            return await context.Identity.Where(q => q.Email.ToLower() == email.ToLower()).Select(u => new Identity()
            {
                Id = u.Id,
                Name = u.Name,
                Password = u.Password
            }).FirstOrDefaultAsync();
        }
    }
}