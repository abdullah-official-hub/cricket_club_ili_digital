using AutoMapper;
using Framework.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PlayerService.Core.Abstractions.Services;
using PlayerService.Core.Domain.Contracts;
using PlayerService.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayerService.Controllers
{
    public class PlayerController : IController
    {
        private readonly IPlayerService playerService;
        private readonly IMapper mapper;

        public PlayerController(IPlayerService playerService, IMapper mapper)
        {
            this.playerService = playerService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<PlayerGetDTO>> AddAsync([FromForm] PlayerPostDTO playerAddDto)
        {
            var player = mapper.Map<Player>(playerAddDto);
            player = await playerService.AddAsync(player,playerAddDto.ProfileImage);
            return mapper.Map<PlayerGetDTO>(player);
        }

        [HttpPut("{id}")]
        public async Task<PlayerGetDTO> UpdateAsync(int id,[FromForm] PlayerPutDTO playerUpdateDto)
        {
            var player = await playerService.UpdateAsync(id, playerUpdateDto);
            return mapper.Map<PlayerGetDTO>(player);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await playerService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<PlayerGetDTO> GetAsync(int id)
        {
            var player = await playerService.GetAsync(id);
            return mapper.Map<PlayerGetDTO>(player);
        }

        [HttpGet]
        public async Task<IEnumerable<PlayerGetDTO>> GetAllAsync(int after, int limit)
        {
            var players = await playerService.GetAllAsync(after, limit);
            return mapper.Map<IEnumerable<PlayerGetDTO>>(players);
        }
    }
}