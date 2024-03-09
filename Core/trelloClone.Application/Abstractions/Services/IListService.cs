using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trelloClone.Domain.Entities;

namespace trelloClone.Application.Abstractions.Services
{
    public interface IListService
    {
        Task CreateList(string title, int boardid);


    }
}
