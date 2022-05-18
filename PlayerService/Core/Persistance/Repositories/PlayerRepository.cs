using Microsoft.EntityFrameworkCore;
using PlayerService.Core.Abstractions.Repositories;
using PlayerService.Core.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerService.Core.Persistance.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly PlayerDbContext context;
        public PlayerRepository(PlayerDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Player player)
        {
            await context.Player.AddAsync(player);
        }

        public void Update(Player player)
        {
            context.Player.Attach(player);
        }

        public async Task<Player> GetAsync(int playerId)
        {
            return await context.Player.Where(q => q.Id == playerId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Player>> GetAllAsync(int after, int limit)
        {
            return await context.Player.Skip(after).Take(limit).OrderByDescending(o => o.Id).ToListAsync();
        }

        public async Task<Player> GetForDeleteAsync(int playerId)
        {
            return await context.Player.Where(q => q.Id == playerId).Select(p => new Player()
            {
                Id = p.Id,
                IsActive = p.IsActive,
            }).FirstOrDefaultAsync();
        }

        public void Remove(Player player)
        {
            context.Player.Remove(player);
        }
    }
}