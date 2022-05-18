namespace IdentityService.Core.Domain.Contracts
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public double ExpiresIn { get; set; }
        public string Type { get; set; }
        public int OwnerId { get; set; }
    }
}