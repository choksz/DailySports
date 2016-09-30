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
    public class PlayerService : IPlayerService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Player> _playerRepository;
        public PlayerService(IUnitOfWork unitOfWork,IGenericRepository<Player> playerRepository)
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
            try
            {
                List<Player> PlayersList = _playerRepository.FindBy(P => P.TeamId == TeamId).ToList();
                List<PlayerDto> PlayerDtoList = new List<PlayerDto>();
                foreach(var Player in PlayersList)
                {
                    PlayerDtoList.Add(new PlayerDto {Id=Player.Id,Name=Player.Name,TeamId=Player.TeamId,TeamName=Player.team.Name });
                }
                return PlayerDtoList;

            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
