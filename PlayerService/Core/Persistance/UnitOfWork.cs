using Framework.Interfaces;
using System.Threading.Tasks;

namespace PlayerService.Core.Persistance
{
    namespace IdentityService.Core.Persistance
    {
        public class UnitOfWork : IUnitOfWork
        {
            private readonly PlayerDbContext context;

            public UnitOfWork(PlayerDbContext context)
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