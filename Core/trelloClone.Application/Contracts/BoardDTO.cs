

namespace trelloClone.Application.Contracts
{
    public class BoardDTO : BaseDto
    {
        public string BoardName { get; set; }
        public List<ListDTO> Lists { get; set; }

    }
}
