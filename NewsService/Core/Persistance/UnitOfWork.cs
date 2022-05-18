using Framework.Interfaces;
using System.Threading.Tasks;

namespace NewsService.Core.Persistance
{
    namespace IdentityService.Core.Persistance
    {
        public class UnitOfWork : IUnitOfWork
        {
            private readonly NewsDbContext context;

            public UnitOfWork(NewsDbContext context)
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