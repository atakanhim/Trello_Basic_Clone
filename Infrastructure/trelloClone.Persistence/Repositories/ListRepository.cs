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
    public class ListRepository : EntityRepository<List>, IListRepository
    {
        public ListRepository(TrelloCloneDbContext dbContext) : base(dbContext)
        {
        }
    }
}
