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
    }
}
