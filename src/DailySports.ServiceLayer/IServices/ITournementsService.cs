using DailySports.ServiceLayer.Dtos;
using System;
using System.Collections.Generic;

namespace DailySports.ServiceLayer.IServices
{
    public interface ITournementsService:IDisposable
    {
        List<TournementsDto> GetAll();
        List<TournementsDto> GetGameTournements(int GameId);
        TournementsDto GetTournement(int Id);
        List<TournementsDto> LatestTournements();
        List<PrizePoolDto> TournametPrizePool(int TournamentId);
        List<GroupStagesDto> TournamentGroupStages(int TournamentId);
        int GetLatestTornamentId();
    }
}
