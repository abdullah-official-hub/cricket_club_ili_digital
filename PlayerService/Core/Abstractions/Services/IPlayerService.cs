using Microsoft.AspNetCore.Http;
using PlayerService.Core.Domain.Contracts;
using PlayerService.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayerService.Core.Abstractions.Services
{
    public interface IPlayerService
    {
        Task<Player> AddAsync(Player player, IFormFile profileImage);
        Task<Player> UpdateAsync(int playerId, PlayerPutDTO playerUpdateDTO);
        Task<Player> GetAsync(int playerId);
        Task<IEnumerable<Player>> GetAllAsync(int after, int limit);
        Task DeleteAsync(int playerId);
    }
}