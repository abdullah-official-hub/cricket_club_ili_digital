using Framework.Exceptions;
using Framework.Interfaces;
using IdentityService.Core.Abstractions.Repositories;
using IdentityService.Core.Abstractions.Services;
using IdentityService.Core.Domain.Contracts;
using IdentityService.Core.Domain.Models;
using IdentityService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Services
{
    public class IdentityServices : IIdentityService
    {
        private readonly IIdentityRepository identityRepository;
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork unitOfWork;

        public IdentityServices(IIdentityRepository identityRepository, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            this.identityRepository = identityRepository;
            this.configuration = configuration;
            this.unitOfWork = unitOfWork;
        }

        public async Task RegisterAsync(Identity identity)
        {
            identity.Password = HashingService.CreateHash(identity.Password);
            await identityRepository.AddAsync(identity);
            await unitOfWork.CompleteAsync();
        }

        public async Task<LoginResponse> AuthenticateAsync(string email, string password)
        {
            var identity = await identityRepository.GetIdentityForAuthentication(email);
            if (identity == null || !HashingService.ValidatePassword(password, identity.Password))
                throw new NotFoundException("Incorrect username or password.");
            return GenerateTokenAsync(identity);
        }

        private LoginResponse GenerateTokenAsync(Identity identity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["CricketClubSettings:AuthEncryptionKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, identity.Id.ToString()),
                    new Claim("OwnerId",identity.Id.ToString()),
                    new Claim("OwnerName",identity.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return new LoginResponse
            {
                Token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)), //Create and write tokken
                ExpiresIn = TimeSpan.FromDays(1).TotalSeconds,
                Type = "bearer",
                OwnerId = identity.Id
            };
        }
    }
}