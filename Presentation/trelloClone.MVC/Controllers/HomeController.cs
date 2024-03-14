using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using trelloClone.Application.Abstractions.Services;
using trelloClone.Application.Contracts;
using trelloClone.MVC.Models;

namespace trelloClone.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;


        public HomeController(ILogger<HomeController> logger, IUserService userService, IAuthService authService)
        {
            _logger = logger;
            _userService = userService;
            _authService = authService;
   
        }

        public IActionResult Index()
        {



            return View();
        }
     
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {

                var token = await _authService.LoginAsync(model.username, model.password);


                _logger.LogInformation("Login Yap�ld�. {Action} {UserName}", "Home/Login",model.username);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["ErrorMessage"] = ex.Message ?? "hatali giris";
                _logger.LogError("Hatal� Login. {Action} denenen isim {UserName}", "Home/Login",model.username);
                return View();
            }

        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(CreateUserDTO model)
        {
            try
            {
                var response = await _userService.CreateAsync(model);
                // Kullan�c� do�ruland�, giri� ba�ar�l�
                // �rnek olarak, bir session olu�turabilirsiniz.
                if(response.Succeeded)
                TempData["SuccessMessage"] = "Ba�ar� ile �ye olundu.";
                else
                {
                    TempData["ErrorMessage"] = response.Message;
                    return View();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewData["ErrorMessage"] = "Hatali giris";    
                return View();
            }

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("logout Yap�ld�. {Action} {UserName}", "Home/Logout", User?.Identity?.Name);
            await _authService.LogoutAsync();
                return RedirectToAction("Index");            
        }      
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

   
    }
}
