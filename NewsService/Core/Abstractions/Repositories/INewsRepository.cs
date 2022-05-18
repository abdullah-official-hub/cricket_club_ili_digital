using NewsService.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsService.Core.Abstractions.Repositories
{
    public interface INewsRepository
    {
        Task AddAsync(News news);
        void Update(News news);
        Task<News> GetAsync(int newsId);
        Task<IEnumerable<News>> GetAllAsync(int after, int limit);
        Task<News> GetForDeleteAsync(int newsId);
        void Remove(News news);
    }
}