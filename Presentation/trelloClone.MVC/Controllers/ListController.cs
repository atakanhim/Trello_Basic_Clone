using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using trelloClone.Application.Abstractions.Services;
using trelloClone.Application.Contracts;
using trelloClone.MVC.Models;

namespace trelloClone.MVC.Controllers
{
    public class ListController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IListService _listService;

        public ListController(IMapper mapper, IListService listService)
        {
            _mapper = mapper;
            _listService = listService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("List/updateListPosition/")]
        public async Task<IActionResult> UpdateCardPosition([FromBody] UpdateListPositionViewModel model)
        {
            UpdateListPositionDTO updateListPositionDTO = _mapper.Map<UpdateListPositionViewModel, UpdateListPositionDTO>(model);

           await _listService.UpdateList(updateListPositionDTO);





            return Ok();
        }
    }
}
