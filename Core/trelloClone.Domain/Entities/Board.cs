

using trelloClone.Domain.Entities.Common;

namespace trelloClone.Domain.Entities
{
    public class Board :BaseEntity
    {
        public string BoardName { get; set; }
        public List<List> Lists { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
