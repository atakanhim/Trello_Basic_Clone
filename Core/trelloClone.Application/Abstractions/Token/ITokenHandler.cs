using trelloClone.Domain.Entities;

namespace trelloClone.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Contracts.Token CreateAccessToken(int accessTokenLifeTimeSecond, AppUser appUser);
        string CreateRefreshToken();
    }
}
