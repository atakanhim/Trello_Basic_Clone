using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trelloClone.Application.Contracts
{
    public class UpdateListPositionDTO
    {
        public int ListId { get; set; }
        public int Position { get; set; }
    }
}
