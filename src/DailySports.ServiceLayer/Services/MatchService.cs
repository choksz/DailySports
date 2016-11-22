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
        private IGenericRepository<Stage> _stagesRepository;

        public MatchService(IUnitOfWork unitOfWork,
                            IGenericRepository<Match> matchRepository,
                            IGenericRepository<Stage> stagesRepository)
        {
            _matchRepository = matchRepository;
            _stagesRepository = stagesRepository;
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
                Match match = _matchRepository.FindBy(M => M.Id == matchId).
                    Include(m => m.Stage).
                    Include(m => m.TeamA).
                        ThenInclude(t => t.Country).
                    Include(m => m.TeamB).
                        ThenInclude(t => t.Country).
                    FirstOrDefault();
                MatchDto matchDto = new MatchDto(match);
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
                /*
                List<ICollection<Match>> Matches = _stagesRepository.FindBy(s => s.TournamentId == TournamentId).Select(s => s.Matches).ToList();
                List<int> matchesIds = new List<int>();
                foreach(Stage s in Stages) {
                    foreach (Match m in s.Matches)
                    {
                        matchesIds.Add(m.Id);
                    }
                }
                List<Match> MatchesList = new List<Match>();
                foreach (int id in matchesIds)
                {
                    Match match = _matchRepository.FindBy(m => m.Id == id).FirstOrDefault();
                }
                foreach (var Match in MatchesList)
                {
                    MatchDtoList.Add(new MatchDto(Match));
                }
                */
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
                /*
                List<Stage> Stages = _stagesRepository.FindBy(s => s.TournamentId == TournamentId).
                    Include(s => s.Matches).
                    ToList();
                List<int> matchesIds = new List<int>();
                List<Match> NextMatchsList = _matchRepository.FindBy(M => M.Date >= DateTime.Today && M.TournamentId == TournamentId).
                    Include(m => m.TeamA).
                    Include(m => m.TeamB).
                    ToList();
                foreach (var Match in NextMatchsList)
                {
                    MatchDtoList.Add(new MatchDto(Match));
                }
                */
            }
            catch (Exception)
            { }
            return MatchDtoList;
        }
    }
}
