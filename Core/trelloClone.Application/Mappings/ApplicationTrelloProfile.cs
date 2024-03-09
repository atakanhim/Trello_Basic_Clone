using AutoMapper;
using trelloClone.Application.Contracts;
using trelloClone.Domain.Entities;



namespace trelloClone.Application.Mappings
{
    public class ApplicationTrelloProfile : Profile
    {
        public ApplicationTrelloProfile()
        {
            CreateMap<Board, BoardDTO>().ReverseMap();
            CreateMap<Card, CardDTO>().ReverseMap();
            CreateMap<List, ListDTO>().ReverseMap();
   

        }
    }
}
