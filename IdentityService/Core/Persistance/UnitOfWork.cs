using Framework.Interfaces;
using System.Threading.Tasks;

namespace IdentityService.Core.Persistance
{
    namespace IdentityService.Core.Persistance
    {
        public class UnitOfWork : IUnitOfWork
        {
            private readonly IdentityDbContext context;

            public UnitOfWork(IdentityDbContext context)
            {
                this.context = context;
            }

            public async Task CompleteAsync()
            {
                await context.SaveChangesAsync();
            }
        }
    }
}