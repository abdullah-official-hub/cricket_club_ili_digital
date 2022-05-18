using AutoMapper;
using IdentityService.Core.Domain.Contracts;
using IdentityService.Core.Domain.Models;

namespace IdentityService.Core.Domain.Mappers
{
    public class IdentityMapper : Profile
    {
        public IdentityMapper()
        {
            CreateMap<Identity, RegisterDTO>().ReverseMap();
            CreateMap<Identity, LoginDTO>().ReverseMap();
        }
    }
}