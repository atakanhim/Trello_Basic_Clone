using trelloClone.Application.Contracts;


namespace trelloClone.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(string usernameOrEmail, string password);
        Task LogoutAsync();
        Task<Contracts.Token> RefreshTokenLoginAsync(string refreshToken, int refreshTokenLifeTimeSecond);
    }
}
