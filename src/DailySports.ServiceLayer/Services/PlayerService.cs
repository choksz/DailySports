using DailySports.ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using DailySports.ServiceLayer.Dtos;
using DailySports.ServiceLayer.UnitOfWork;
using DailySports.ServiceLayer.Repositories.Core;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Services
{
    public class PlayerService : IPlayerService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Player> _playerRepository;
        public PlayerService(IUnitOfWork unitOfWork, IGenericRepository<Player> playerRepository)
        {
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public List<PlayerDto> GetTeamPlayers(int TeamId)
        {
            List<PlayerDto> PlayerDtoList = new List<PlayerDto>();
            try
            {
                List<Player> PlayersList = _playerRepository.FindBy(P => P.TeamId == TeamId).ToList();
                
                foreach(var Player in PlayersList)
                {
                    PlayerDtoList.Add(new PlayerDto(Player));
                }
                

            }
            catch(Exception)
            { }
            return PlayerDtoList;
        }
    }
}
