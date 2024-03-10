using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trelloClone.Application.Abstractions.Services;
using trelloClone.Application.Contracts;
using trelloClone.Domain.Entities;
using trelloClone.MVC.Models;
using trelloClone.Persistence.Services;

namespace trelloClone.MVC.Controllers
{
  //  [Authorize]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;

        public CardController(ICardService cardService, IMapper mapper)
        {
            _cardService = cardService;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("Card/CreateCard")]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardViewModel model)
        {

            await _cardService.CreateCard(model.Title,model.Description,model.ListId);



            return Ok("Eklendi");
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("Card/DeleteCard/{cardId?}")]
        public async Task<IActionResult> DeleteList(int cardId)
        {

            bool result = await _cardService.DeleteCard(cardId);

            return Ok(result);
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
