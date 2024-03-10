using trelloClone.Application.Contracts;

namespace trelloClone.MVC.Models
{
    public class ListListViewModel
    {
        public string Title { get; set; }
        public int Position { get; set; } // Sıralama bilgisi
        public List<CardDTO> Cards { get; set; }
        public int BoardId { get; set; }

    }
}
