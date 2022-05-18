using AutoMapper;
using Framework.Exceptions;
using Framework.Interfaces;
using Microsoft.AspNetCore.Http;
using PlayerService.Core.Abstractions.Repositories;
using PlayerService.Core.Abstractions.Services;
using PlayerService.Core.Domain.Contracts;
using PlayerService.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayerService.Core.Services
{
    public class PlayerServices : IPlayerService
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public PlayerServices(IPlayerRepository playerRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.playerRepository = playerRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Player> AddAsync(Player player, IFormFile profileImage)
        {
            // Upload file to ftp server like s3 or anyother and save the url in player model
            player.ProfileImageUrl = "FTP Server Image Url";
            await playerRepository.AddAsync(player);
            await unitOfWork.CompleteAsync();
            return player;
        }

        public async Task<Player> UpdateAsync(int playerId, PlayerPutDTO playerUpdateDTO)
        {
            var dbPlayer = await playerRepository.GetAsync(playerId);
            if (dbPlayer == null)
            {
                throw new NotFoundException("Player not found.");
            }
            // Upload file to ftp server from playerUpdateDTO.ProfileImage like s3 or anyother and save the updated url in player model
            dbPlayer.ProfileImageUrl = "FTP Server Image Url Updated";
            dbPlayer = mapper.Map(playerUpdateDTO, dbPlayer);
            await unitOfWork.CompleteAsync();
            return dbPlayer;
        }

        public async Task<Player> GetAsync(int playerId)
        {
            var player = await playerRepository.GetAsync(playerId);
            if (player == null)
            {
                throw new NotFoundException("Player not found.");
            }
            return player;
        }

        public async Task<IEnumerable<Player>> GetAllAsync(int after, int limit)
        {
            return await playerRepository.GetAllAsync(after, limit);
        }

        public async Task DeleteAsync(int playerId)
        {
            var dbPlayer = await playerRepository.GetForDeleteAsync(playerId);
            if (dbPlayer == null)
            {
                throw new NotFoundException("Player not found.");
            }
            playerRepository.Remove(dbPlayer);
            await unitOfWork.CompleteAsync();
        }
    }
}