using Microsoft.EntityFrameworkCore;
using NewsService.Core.Abstractions.Repositories;
using NewsService.Core.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsService.Core.Persistance.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsDbContext context;
        public NewsRepository(NewsDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(News news)
        {
            await context.News.AddAsync(news);
        }

        public void Update(News news)
        {
            context.News.Attach(news);
        }

        public async Task<News> GetAsync(int newsId)
        {
            return await context.News.Where(q => q.Id == newsId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<News>> GetAllAsync(int after, int limit)
        {
            return await context.News.Skip(after).Take(limit).OrderByDescending(o => o.Id).ToListAsync();
        }

        public async Task<News> GetForDeleteAsync(int newsId)
        {
            return await context.News.Where(q => q.Id == newsId).Select(p => new News()
            {
                Id = p.Id,
                IsActive = p.IsActive,
            }).FirstOrDefaultAsync();
        }

        public void Remove(News news)
        {
            context.News.Remove(news);
        }
    }
}