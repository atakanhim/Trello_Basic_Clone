

namespace trelloClone.Application.Contracts
{
    public class CardDTO : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Position { get; set; } // Sıralama bilgisi
        public int ListId { get; set; }

    }
}
