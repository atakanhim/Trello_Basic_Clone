using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trelloClone.Domain.Entities.Common;

namespace trelloClone.Domain.Entities
{
    public class List:BaseEntity
    {
        public string Title { get; set; }
        public int Position { get; set; } =  0;// Sıralama bilgisi
        public List<Card> Cards { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}
