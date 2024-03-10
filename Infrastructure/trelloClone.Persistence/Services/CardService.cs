using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trelloClone.Application.Abstractions.Services;
using trelloClone.Application.Contracts;
using trelloClone.Application.Repositories;
using trelloClone.Domain.Entities;

namespace trelloClone.Persistence.Services
{
    public class CardService:ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IListRepository _listRepository;
        private readonly IMapper _mapper;

        public CardService(ICardRepository cardRepository, IListRepository listRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _listRepository = listRepository;
            _mapper = mapper;
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

        public async Task<IEnumerable<ListDTO>> UpdateCard(UpdateCardPositionDTO updateCard)
        {
            try
            {
                var newCardIndex = updateCard.Position;
                Card? oldCard = await _cardRepository.GetAsync(x => x.Id == updateCard.CardId);
                var list = await _listRepository.Table.Include(y => y.Cards.OrderBy(z => z.Position)).Where(c => c.Id == updateCard.ListId).FirstOrDefaultAsync();                
                if (oldCard?.ListId == updateCard.ListId)
                {
                    await fonksiyon(oldCard.Position,newCardIndex,updateCard.CardId,list);
                }
                else
                {
                    // eger baska bir listeye ekliyorsa ve kendi listesinden siliniyorsa
                    var oldList = await _listRepository.Table.Include(y => y.Cards.OrderBy(z => z.Position)).Where(c => c.Id == oldCard.ListId).FirstOrDefaultAsync();
                    var newList = await _listRepository.Table.Include(y => y.Cards.OrderBy(z => z.Position)).Where(c => c.Id == updateCard.ListId).FirstOrDefaultAsync();

                    // listeid degisistiriyorum
                        oldCard.ListId = newList.Id;
                        await _listRepository.SaveAsync();

                    // Eski listedeki kartların pozisyonlarını güncelle
                        int newPosition = 0;
                        foreach (var card in oldList.Cards)
                        {
                            card.Position = newPosition;
                            newPosition++;
                        }
                        await _listRepository.SaveAsync();

                    // yeni listesine eklenecek 

                     if(newList.Cards.Count > 1)
                    {
                        // mesela gideci liste 4 elemanlı 0-1-2-3   be ben bunların arasına 1 vs 2 arasına koycam o zaman ne olcak ,
                        // o zaman index 2 olursa 2 den sonraki indexler 1 artıcak nereye koyarsam ondan sonraki indexler artsın 
                        foreach (var card in newList.Cards.Where(c => c.Position >= newCardIndex))
                        {
                            card.Position += 1;
                            _cardRepository.Update(card);
                            await _cardRepository.SaveAsync();

                        }
                        Card newCartt = await _cardRepository.GetAsync(x => x.Id == oldCard.Id);
                        newCartt.Position = newCardIndex;
                        await _cardRepository.SaveAsync();
                    }
                    else
                    {
                        Card newCart = await _cardRepository.GetAsync(x => x.Id == oldCard.Id);
                        newCart.Position = 0;
                        await _cardRepository.SaveAsync();
                    }

                }

                IEnumerable<List> lists= await _listRepository.Table.Include(y => y.Cards.OrderBy(z => z.Position)).ToListAsync();
                IEnumerable<ListDTO> listsDTO = _mapper.Map<IEnumerable<List>, IEnumerable<ListDTO>>(lists);
                return listsDTO;
            }
            catch (Exception ex)
            {
                throw;
            }




           
        }
        private async Task fonksiyon(int oldCardIndex,int newCardIndex,int CardId,List list)
        {
            var step = (oldCardIndex < newCardIndex) ? -1 : 1;
            var start = (oldCardIndex < newCardIndex) ? oldCardIndex + 1 : newCardIndex;
            var end = (oldCardIndex < newCardIndex) ? newCardIndex : oldCardIndex - 1;


            foreach (var card in list.Cards.Where(c => c.Position >= start && c.Position <= end))
            {
                card.Position += step;
                _cardRepository.Update(card);
                await _cardRepository.SaveAsync();

            }

            Card newCart = await _cardRepository.GetAsync(x => x.Id == CardId);
            newCart.Position = newCardIndex;
            await _cardRepository.SaveAsync();
        }
    }
}
