using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trelloClone.Application.Contracts;
using trelloClone.Domain.Entities;

namespace trelloClone.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<bool> IsUserExists(string usurId);
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshTokenAsync(string? refreshToken, AppUser user, DateTime? accessTokenDate, int addOnAccessTokenDate);

        int TotalUsersCount { get; }
    }
}
