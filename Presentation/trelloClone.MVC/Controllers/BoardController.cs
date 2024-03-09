using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trelloClone.Application.Abstractions.Services;
using trelloClone.Application.Contracts;
using trelloClone.Domain.Entities;
using trelloClone.MVC.Models;

namespace trelloClone.MVC.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private readonly IBoardService _boardService;
        private readonly IMapper _mapper;

        public BoardController(IBoardService boardService, IMapper mapper)
        {
            _boardService = boardService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            


           IEnumerable<BoardDTO> listDTO = await _boardService.GetBoard();
            IEnumerable<ListBoardViewModel> listViewModel = _mapper.Map<IEnumerable<BoardDTO>, IEnumerable<ListBoardViewModel>>(listDTO);


            return View();
        }

        public IActionResult BoardDetail()
        {
            // Token'ı session'dan alın
            var token = HttpContext.Session.GetString("Token");

            // Token ile yetkilendirme işlemi yapın

            return View();
        }

    }
}
