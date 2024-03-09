using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trelloClone.Application.Contracts;

namespace trelloClone.Application.Abstractions.Services
{
    public interface ICardService
    {
        Task CreateCard(string title, string desc, int listId);
        Task UpdateCard(UpdateCardPositionDTO updateCard);
    }
}
