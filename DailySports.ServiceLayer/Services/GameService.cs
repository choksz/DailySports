using DailySports.ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailySports.ServiceLayer.Dtos;
using DailySports.ServiceLayer.UnitOfWork;
using DailySports.ServiceLayer.Repositories.Core;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Services
{
    public class GameService : IGameService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Game> _gameRepository;
        public GameService(IUnitOfWork unitOfWork,IGenericRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
            _unitOfWork = unitOfWork;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public List<GameDto> GetAll()
        {
           try
            {
                List<Game> GameList = _gameRepository.GetAll().ToList();
                List<GameDto> GameDtoList = new List<GameDto>();
                foreach(var game in GameList)
                {
                    GameDtoList.Add(new GameDto {Id=game.Id,Name=game.Name,GameImage=game.GameImage,LiveStreamURL=game.LiveStreamUrl });
                }
                return GameDtoList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

       
    }
}
