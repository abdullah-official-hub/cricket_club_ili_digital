using AutoMapper;
using Framework.Exceptions;
using Framework.Interfaces;
using Microsoft.AspNetCore.Http;
using NewsService.Core.Abstractions.Repositories;
using NewsService.Core.Abstractions.Services;
using NewsService.Core.Domain.Contracts;
using NewsService.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsService.Core.Services
{
    public class NewsServices : INewsService
    {
        private readonly INewsRepository newsRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public NewsServices(INewsRepository newsRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.newsRepository = newsRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<News> AddAsync(News news, IFormFile newsImage)
        {
            // Upload file to ftp server like s3 or anyother and save the url in news model
            news.NewsImageUrl = "FTP Server Image Url";
            await newsRepository.AddAsync(news);
            await unitOfWork.CompleteAsync();
            return news;
        }

        public async Task<News> UpdateAsync(int newsId, NewsPutDTO newsUpdateDTO)
        {
            var dbNews = await newsRepository.GetAsync(newsId);
            if (dbNews == null)
            {
                throw new NotFoundException("News not found.");
            }
            // Upload file to ftp server from newsUpdateDTO.NewsImage like s3 or anyother and save the updated url in news model
            dbNews.NewsImageUrl = "FTP Server Image Url Updated";
            dbNews = mapper.Map(newsUpdateDTO, dbNews);
            await unitOfWork.CompleteAsync();
            return dbNews;
        }

        public async Task<News> GetAsync(int newsId)
        {
            var news = await newsRepository.GetAsync(newsId);
            if (news == null)
            {
                throw new NotFoundException("News not found.");
            }
            return news;
        }

        public async Task<IEnumerable<News>> GetAllAsync(int after, int limit)
        {
            return await newsRepository.GetAllAsync(after, limit);
        }

        public async Task DeleteAsync(int newsId)
        {
            var dbNews = await newsRepository.GetForDeleteAsync(newsId);
            if (dbNews == null)
            {
                throw new NotFoundException("News not found.");
            }
            newsRepository.Remove(dbNews);
            await unitOfWork.CompleteAsync();
        }
    }
}