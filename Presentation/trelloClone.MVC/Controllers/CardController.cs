using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using trelloClone.Application.Abstractions.Services;
using trelloClone.Application.Contracts;
using trelloClone.Domain.Entities;
using trelloClone.MVC.Models;

namespace trelloClone.MVC.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;

        public CardController(ICardService cardService, IMapper mapper)
        {
            _cardService = cardService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Card/updateCardPosition/")]
        public async Task<IActionResult> UpdateCardPosition([FromBody] UpdateCardPositionViewModel model)
        {
            UpdateCardPositionDTO updateCardPositionDTO = _mapper.Map<UpdateCardPositionViewModel, UpdateCardPositionDTO>(model);
            IEnumerable<ListDTO> listdto = await _cardService.UpdateCard(updateCardPositionDTO);

            IEnumerable<ListListViewModel> listViewmodel = _mapper.Map<IEnumerable<ListDTO>,IEnumerable<ListListViewModel> >(listdto);




            return Ok(listViewmodel);
        }
    }
}
