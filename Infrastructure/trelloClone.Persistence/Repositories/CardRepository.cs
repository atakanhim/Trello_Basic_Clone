using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trelloClone.Application.Repositories;
using trelloClone.Domain.Entities;
using trelloClone.Persistence.Context;

namespace trelloClone.Persistence.Repositories
{
    public class CardRepository : EntityRepository<Card>, ICardRepository
    {
        public CardRepository(TrelloCloneDbContext dbContext) : base(dbContext)
        {
        }
    }
}
