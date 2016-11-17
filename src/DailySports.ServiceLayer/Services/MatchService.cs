using DailySports.ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using DailySports.ServiceLayer.Dtos;
using DailySports.ServiceLayer.UnitOfWork;
using DailySports.ServiceLayer.Repositories.Core;
using DailySports.DataLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DailySports.ServiceLayer.Services
{
    public class MatchService : IMatchService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Match> _matchRepository;

        public MatchService(IUnitOfWork unitOfWork, IGenericRepository<Match> matchRepository)
        {
            _matchRepository = matchRepository;
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public MatchDto GetMatch(int matchId)
        {
            try
            {
                Match match = _matchRepository.FindBy(M => M.Id == matchId).FirstOrDefault();
                MatchDto matchDto = new MatchDto();
                matchDto.Id = match.Id;
                matchDto.Date = match.Date;
                matchDto.TeamAId = match.TeamAId;
                matchDto.TeamAName = match.TeamA.Name;
                matchDto.TeamBId = match.TeamBId;
                matchDto.TeamBName = match.TeamB.Name;
                matchDto.TournamentId = match.TournamentId;
                matchDto.TournamentName = match.Tournament.Title;

                return matchDto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<MatchDto> TournamentMatches(int TournamentId)
        {
            List<MatchDto> MatchDtoList = new List<MatchDto>();
            try
            {
                List<Match> MatchesList = _matchRepository.FindBy(M => M.TournamentId == TournamentId).
                    Include(m => m.TeamA).
                        ThenInclude(t => t.GroupStage).
                    Include(m => m.TeamB).
                        ThenInclude(t => t.GroupStage).
                    Include(m => m.Tournament).
                    ToList();
                foreach (var Match in MatchesList)
                {
                    MatchDtoList.Add(new MatchDto
                    {
                        Id = Match.Id,
                        Date = Match.Date,
                        TeamAId = Match.TeamAId,
                        TeamAName = Match.TeamA.Name,
                        TeamBId = Match.TeamBId,
                        TeamBName = Match.TeamB.Name,
                        TournamentId = Match.TournamentId,
                        TournamentName = Match.Tournament.Title,
                        TeamA = new TeamDto
                        {
                            Id = Match.TeamA.Id,
                            Name = Match.TeamA.Name,
                            Logo = Match.TeamA.Logo
                        },
                        TeamB = new TeamDto
                        {
                            Id = Match.TeamB.Id,
                            Name = Match.TeamB.Name,
                            Logo = Match.TeamB.Logo
                        }
                    }
                    );
                }
            }
            catch (Exception)
            { }
            return MatchDtoList;
        }

        public List<MatchDto> NextMatches(int TournamentId)
        {
            List<MatchDto> MatchDtoList = new List<MatchDto>();
            try
            {
                List<Match> NextMatchsList = _matchRepository.FindBy(M => M.Date >= DateTime.Today && M.TournamentId == TournamentId).
                    Include(m => m.TeamA).
                    Include(m => m.TeamB).
                    Include(m => m.Tournament).ToList();
                foreach (var Match in NextMatchsList)
                {
                    MatchDtoList.Add(new MatchDto
                        {
                            Id = Match.Id,
                            Date = Match.Date,
                            TeamAId = Match.TeamAId,
                            TeamAName = Match.TeamA.Name,
                            TeamBId = Match.TeamBId,
                            TeamBName = Match.TeamB.Name,
                            TournamentId = Match.TournamentId,
                            TournamentName =
                            Match.Tournament.Title
                        }
                    );
                }
                
            }
            catch (Exception)
            { }
            return MatchDtoList;
        }
    }
}
