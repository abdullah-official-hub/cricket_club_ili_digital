using AutoMapper;
using NewsService.Core.Domain.Contracts;
using NewsService.Core.Domain.Models;

namespace NewsService.Core.Domain.Mappers
{
    public class NewsMapper : Profile
    {
        public NewsMapper()
        {
            CreateMap<News, NewsPostDTO>().ReverseMap();
            CreateMap<News, NewsPutDTO>().ReverseMap();
            CreateMap<News, NewsGetDTO>().ReverseMap();
        }
    }
}