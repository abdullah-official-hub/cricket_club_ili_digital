using Microsoft.AspNetCore.Http;
using NewsService.Core.Domain.Contracts;
using NewsService.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsService.Core.Abstractions.Services
{
    public interface INewsService
    {
        Task<News> AddAsync(News news, IFormFile newsImage);
        Task<News> UpdateAsync(int newsId, NewsPutDTO newsUpdateDTO);
        Task<News> GetAsync(int newsId);
        Task<IEnumerable<News>> GetAllAsync(int after, int limit);
        Task DeleteAsync(int newsId);
    }
}