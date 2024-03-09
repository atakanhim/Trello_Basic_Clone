using trelloClone.Application.Contracts;

namespace trelloClone.MVC.Models
{
    public class ListBoardIncludeViewModel
    {
        public string BoardName { get; set; }
        public List<ListDTO> Lists { get; set; }
    }
}
