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
                var list = new List() { BoardId = boardid, Title = title};
                await _listRepository.AddAsync(list);
                await _listRepository.SaveAsync();

            }
            catch (Exception)
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
