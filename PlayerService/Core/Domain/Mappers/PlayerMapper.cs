using AutoMapper;
using PlayerService.Core.Domain.Contracts;
using PlayerService.Core.Domain.Models;

namespace PlayerService.Core.Domain.Mappers
{
    public class PlayerMapper : Profile
    {
        public PlayerMapper()
        {
            CreateMap<Player, PlayerPostDTO>().ReverseMap();
            CreateMap<Player, PlayerPutDTO>().ReverseMap();
            CreateMap<Player, PlayerGetDTO>().ReverseMap();
        }
    }
}