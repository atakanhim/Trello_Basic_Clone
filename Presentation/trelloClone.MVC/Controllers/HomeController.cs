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
        public async Task<IActionResult> CreateUser([FromBody] CreateUser model)
        {

            var response = await _userService.CreateAsync(model);
            // Kullanýcý doðrulandý, giriþ baþarýlý
            // Örnek olarak, bir session oluþturabilirsiniz.



            return Ok(response);

        }
        [HttpPost]
        [Route("CreateBoard")]
        public async Task<IActionResult> CreateBoard([FromBody] CreateBoardViewModel model)
        {
            await _boardService.CreateBoard(model.BoardName, model.AppUserId);
            // Kullanýcý doðrulandý, giriþ baþarýlý
            // Örnek olarak, bir session oluþturabilirsiniz.



            return Ok("Eklendi");

        }    
  
        [HttpPost]
        [Route("CreateCard")]
        public async Task<IActionResult> CrateCard([FromBody] CreateCardViewModel model)
        {

            await _cardService.CreateCard(model.Title,model.Description, model.ListId);



            return Ok("Eklendi");

        }
        /// <summary>
        /// api bitis
        /// </summary>
        /// <returns></returns>
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
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["ErrorMessage"] = ex.Message??"hatali giris" ;
                return View();
            }
      
        }          
        [HttpPost]
        public async Task<IActionResult> Logout()
        {          
                await _authService.LogoutAsync();
                return RedirectToAction("Privacy");            
        }      
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

   
    }
}
