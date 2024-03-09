


namespace trelloClone.Application.Contracts
{
    public class ListDTO : BaseDto
    {
        public string Title { get; set; }
        public int Position { get; set; } // Sıralama bilgisi
        public List<CardDTO> Cards { get; set; }
        public int BoardId { get; set; }


    }
}
