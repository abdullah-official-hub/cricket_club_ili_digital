using AutoMapper;
using Framework.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NewsService.Core.Abstractions.Services;
using NewsService.Core.Domain.Contracts;
using NewsService.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsService.Controllers
{
    public class NewsController : IController
    {
        private readonly INewsService newsService;
        private readonly IMapper mapper;

        public NewsController(INewsService newsService, IMapper mapper)
        {
            this.newsService = newsService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<NewsGetDTO>> AddAsync([FromForm] NewsPostDTO newsAddDto)
        {
            var news = mapper.Map<News>(newsAddDto);
            news = await newsService.AddAsync(news, newsAddDto.NewsImage);
            return mapper.Map<NewsGetDTO>(news);
        }

        [HttpPut("{id}")]
        public async Task<NewsGetDTO> UpdateAsync(int id, NewsPutDTO newsUpdateDto)
        {
            var news = await newsService.UpdateAsync(id, newsUpdateDto);
            return mapper.Map<NewsGetDTO>(news);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await newsService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<NewsGetDTO> GetAsync(int id)
        {
            var news = await newsService.GetAsync(id);
            return mapper.Map<NewsGetDTO>(news);
        }

        [HttpGet]
        public async Task<IEnumerable<NewsGetDTO>> GetAllAsync(int after, int limit)
        {
            var newss = await newsService.GetAllAsync(after, limit);
            return mapper.Map<IEnumerable<NewsGetDTO>>(newss);
        }
    }
}