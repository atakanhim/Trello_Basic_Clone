using Microsoft.AspNetCore.Identity;

using AutoMapper;

using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using trelloClone.Application.Abstractions.Services;
using trelloClone.Application.Contracts;
using trelloClone.Domain.Entities;
using trelloClone.Application.Exceptions;

namespace trelloClone.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<Domain.Entities.AppUser> _userManager;
        private IMapper _mapper;
        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public int TotalUsersCount => throw new NotImplementedException();
        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                RefreshToken = "",// create aşamasında bos olamaz hatası alıyoruz 
                RefreshTokenEndDate = DateTime.UtcNow,
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
            {
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            }

            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }
        public async Task<bool> IsUserExists(string usurId)
        {
            var model = await _userManager.FindByIdAsync(usurId);
            return model != null;
        }
        public async Task UpdateRefreshTokenAsync(string? refreshToken, AppUser user, DateTime? accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = (DateTime)(accessTokenDate?.AddSeconds(addOnAccessTokenDate));

                await _userManager.UpdateAsync(user);
            }
            else
                throw new AuthenticationErrorException();
        }

    }
}
