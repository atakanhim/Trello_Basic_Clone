using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trelloClone.Application.Abstractions.Services;
using trelloClone.Application.Repositories;
using trelloClone.Domain.Entities;

namespace trelloClone.Persistence.Services
{
    public class CardService:ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task CreateCard(string title,string desc, int listId)
        {
            try
            {
                var card = new Card() { ListId = listId, Title = title ,Description = desc};
                await _cardRepository.AddAsync(card);
                await _cardRepository.SaveAsync();

            }
            catch (Exception)
            {

                throw;

            }
        }
    }
}
