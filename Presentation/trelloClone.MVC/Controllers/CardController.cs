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

            await _cardService.UpdateCard(updateCardPositionDTO);
            return Ok();
        }
    }
}
