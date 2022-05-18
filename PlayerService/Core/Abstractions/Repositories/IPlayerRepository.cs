using PlayerService.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayerService.Core.Abstractions.Repositories
{
    public interface IPlayerRepository
    {
        Task AddAsync(Player player);
        void Update(Player player);
        Task<Player> GetAsync(int playerId);
        Task<IEnumerable<Player>> GetAllAsync(int after, int limit);
        Task<Player> GetForDeleteAsync(int playerId);
        void Remove(Player player);
    }
}