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
    public class BoardRepository : EntityRepository<Board>, IBoardRepository
    {
        public BoardRepository(TrelloCloneDbContext dbContext) : base(dbContext)
        {
        }
    }
}
