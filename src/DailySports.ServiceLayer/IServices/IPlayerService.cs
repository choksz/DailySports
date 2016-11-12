using DailySports.ServiceLayer.Dtos;
using System;
using System.Collections.Generic;

namespace DailySports.ServiceLayer.IServices
{
    public interface IPlayerService:IDisposable
    {
        List<PlayerDto> GetTeamPlayers(int TeamId);
    }
}
