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
        private readonly IBoardService _boardService;
        private readonly IListService _listService;
        private readonly ICardService _cardService;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IUserService userService, HttpClient httpClient, IAuthService authService, IBoardService boardService, IListService listService, ICardService cardService)
        {
            _logger = logger;
            _userService = userService;
            _httpClient = httpClient;
            _authService = authService;
            _boardService = boardService;
            _listService = listService;
            _cardService = cardService;
        }

        // api 
        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserViewModel model)
        {

            var response = await _userService.CreateAsync(new CreateUserDTO { Email = model.Email,Username=model.Username,Password=model.Password,PasswordConfirm=model.PasswordConfirm});
            // Kullanýcý doðrulandý, giriþ baþarýlý
            // Örnek olarak, bir session oluþturabilirsiniz.



            return Ok(response);

        }
        public IActionResult Index()
        {
            _logger.LogInformation("Bu bir bilgi mesajýdýr. {Category}", "Home");



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
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["ErrorMessage"] = ex.Message ?? "hatali giris";
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
                // Kullanýcý doðrulandý, giriþ baþarýlý
                // Örnek olarak, bir session oluþturabilirsiniz.
                if(response.Succeeded)
                TempData["SuccessMessage"] = "Baþarý ile üye olundu.";
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
 
        [HttpPost]
        public async Task<IActionResult> Logout()
        {          
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
