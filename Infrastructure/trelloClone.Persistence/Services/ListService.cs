using Azure.Core.GeoJson;
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
    public class ListService : IListService
    {
        private readonly IListRepository _listRepository;

        public ListService(IListRepository listRepository)
        {
            _listRepository = listRepository;
        }
        public async Task CreateList(string title, int boardid)
        {
            try
            {
                var list = new List() { BoardId = boardid, Title = title };

                var listList = await _listRepository.GetListAsync();
                if(!(listList == null || listList.Count == 0))
                {
                    var maksPozisyon = listList.Max(x => x.Position);
                    list.Position = maksPozisyon + 1;
                }
  
                await _listRepository.AddAsync(list);
                await _listRepository.SaveAsync();


            }
            catch (Exception)
            {

                throw  new Exception("Liste olsturma başarısız");

            }
        }

        public async Task<bool> DeleteList(int listId)
        {
            try
            {
                var list = await _listRepository.Table.Include(x => x.Cards).Where(x=>x.Id == listId).FirstOrDefaultAsync();
                if(list == null)
                    throw new Exception("Liste Bulunamadı");

                var cardsCount = list.Cards.Count();
                if (cardsCount > 0)
                    throw new Exception("Liste dolu, Dolu liste silinemez");


                else
                {
                    _listRepository.Delete(list);
                    await _listRepository.SaveAsync();

                    var listArray = await _listRepository.Table.OrderBy(x => x.Position).ToListAsync(); 
                    int newPosition = 0;
                    foreach(var listItem in listArray)
                    {
                        listItem.Position = newPosition;
                        newPosition++;
                        await _listRepository.SaveAsync();
                    }
                    return true;
                }
         
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public async Task UpdateList(UpdateListPositionDTO updateList)
        {
            try
            {
                List? oldlist = await _listRepository.GetAsync(x => x.Id == updateList.ListId);

                IEnumerable<List> list = await _listRepository.GetListAsync();

                int oldListIndex = oldlist.Position;
                int newListIndex = updateList.Position;

                var step = (oldListIndex < newListIndex) ? -1 : 1;
                var start = (oldListIndex < newListIndex) ? oldListIndex + 1 : newListIndex;
                var end = (oldListIndex < newListIndex) ? newListIndex : oldListIndex - 1;




                foreach (var listItem in list.Where(c => c.Position >= start && c.Position <= end))
                {
                    listItem.Position += step;
                    _listRepository.Update(listItem);
                    await _listRepository.SaveAsync();

                }

                List? newList = await _listRepository.GetAsync(x => x.Id == updateList.ListId);
                newList.Position = newListIndex;
                await _listRepository.SaveAsync();
            }
            catch (Exception)
            {
                throw;

            }


        }
    }
}
