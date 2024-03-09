using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using trelloClone.Application.Abstractions.Services;
using trelloClone.Application.Abstractions.Token;
using trelloClone.Application.Contracts;
using trelloClone.Application.Exceptions;
using trelloClone.Domain.Entities;

namespace trelloClone.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly IConfiguration _configuration;
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<AppUser> _signInManager;
        readonly IUserService _userService;
        public AuthService(
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            ITokenHandler tokenHandler,
            SignInManager<AppUser> signInManager,
            IUserService userService
         )
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;

        }
        

        public async Task<LoginResponseDTO> LoginAsync(string usernameOrEmail, string password)
        {
            AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new AuthenticationErrorException();

            SignInResult result = await _signInManager.PasswordSignInAsync(user, password, false,lockoutOnFailure:false);
            if (result.Succeeded) //Authentication başarılı!
            {
                Token tokenn = _tokenHandler.CreateAccessToken(76500, user);
                await _userService.UpdateRefreshTokenAsync(tokenn.RefreshToken, user, tokenn.Expiration, 76500);
                return new (){ 
                    token = tokenn,
                    username=user.UserName
                };
            }
            throw new AuthenticationErrorException();
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<Token> RefreshTokenLoginAsync(string refreshToken, int second)// var olan refreh token kullnarak yeni bir token olusturur ve onunda üzerine koyarak yeni bir resfresh token olusturur.
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(second, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, second);
                return token;
            }
            else
                throw new Exception("user bulunamadi");
        }

    }
}
