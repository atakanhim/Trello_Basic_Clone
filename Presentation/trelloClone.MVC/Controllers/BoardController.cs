using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trelloClone.Application.Abstractions.Services;
using trelloClone.Application.Contracts;
using trelloClone.Domain.Entities;
using trelloClone.MVC.Models;

namespace trelloClone.MVC.Controllers
{
   // [Authorize]
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


            return View(listViewModel);
        }

        public async Task<IActionResult> BoardDetail(int id)
        {
            ViewBag.BoardId = id;

            return View();
        }

        [HttpGet]
        [Route("Board/GetBoardWithIncludes/{boardId?}")]
        public async Task<IActionResult> GetBoardWithIncludes( int boardId)
        {
            var board = await _boardService.GetBoardWithIncludes(boardId);
            var listViewModel = _mapper.Map<BoardDTO, ListBoardIncludeViewModel>(board);
            return Ok(listViewModel);
        }
    }
}
