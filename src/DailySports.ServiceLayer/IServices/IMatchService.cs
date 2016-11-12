using DailySports.ServiceLayer.Dtos;
using System;
using System.Collections.Generic;

namespace DailySports.ServiceLayer.IServices
{
    public interface IMatchService : IDisposable
    {
        List<MatchDto> TournamentMatches(int TournamentId);
        MatchDto GetMatch(int matchId);
        List<MatchDto> NextMatches(int TournamentId);
    }
}
