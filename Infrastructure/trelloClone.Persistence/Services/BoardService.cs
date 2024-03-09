using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trelloClone.Application.Abstractions.Services;
using trelloClone.Application.Contracts;
using trelloClone.Application.Repositories;
using trelloClone.Domain.Entities;

namespace trelloClone.Persistence.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;

        public BoardService(IBoardRepository boardRepository, IMapper mapper)
        {
            _boardRepository = boardRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BoardDTO>> GetBoard()
        {
            try
            {

                IEnumerable<Board> list = await _boardRepository.GetListAsync();
                IEnumerable<BoardDTO> listDTO = _mapper.Map<IEnumerable<Board>, IEnumerable<BoardDTO>>(list);
                return listDTO;

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task CreateBoard(string boardName, string appUserId)
        {
            try
            {
                var board = new Board() { AppUserId = appUserId, BoardName = boardName };
                await _boardRepository.AddAsync(board);
                await _boardRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;

            }
        }
    }
}
