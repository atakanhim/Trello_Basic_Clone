using AutoMapper;
using trelloClone.Application.Contracts;
using trelloClone.Domain.Entities;
using trelloClone.MVC.Models;


namespace trelloClone.Presentation.Mappings
{
    public class PresentationTrelloProfile : Profile
    {
        public PresentationTrelloProfile()
        {
            CreateMap<ListBoardViewModel, BoardDTO>().ReverseMap();
            CreateMap<ListBoardIncludeViewModel, BoardDTO>().ReverseMap();
            CreateMap<UpdateCardPositionViewModel, UpdateCardPositionDTO>().ReverseMap();


        }

    }
}
