using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trelloClone.Application.Contracts;
using trelloClone.Application.Repositories;
using trelloClone.Domain.Entities;

namespace trelloClone.Application.Abstractions.Services
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardDTO>> GetBoard();
        Task CreateBoard(string boardName, string appUserId);
    }
}
