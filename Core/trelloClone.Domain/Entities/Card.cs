
using trelloClone.Domain.Entities.Common;

namespace trelloClone.Domain.Entities
{
    public class Card:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; } = 0; // Sıralama bilgisi
         
        public int ListId { get; set; }
        public List List { get; set; }
    }
}
