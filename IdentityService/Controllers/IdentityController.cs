using AutoMapper;
using Framework.Interfaces;
using IdentityService.Core.Abstractions.Services;
using IdentityService.Core.Domain.Contracts;
using IdentityService.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityService.Controllers
{
    public class IdentityController : IController
    {
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public IdentityController(IIdentityService identityService, IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<string>> RegisterAsync(RegisterDTO registerDTO)
        {
            var identity = mapper.Map<Identity>(registerDTO);
            await identityService.RegisterAsync(identity);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> AuthenticateAsync(LoginDTO loginDTO)
        {
            var authenticationResponse = await identityService.AuthenticateAsync(loginDTO.Email,loginDTO.Password);
            return authenticationResponse;
        }

        [HttpPost]
        public void Logout()
        {
            // Discussion required due to api gateway scenario is changed
        }
    }
}