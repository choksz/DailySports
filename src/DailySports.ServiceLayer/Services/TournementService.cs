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
    public class TournementService : ITournementsService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Tournaments> _tournamentsRepository;
        private IGenericRepository<PrizePool> _prizePoolRepository;
        private IGenericRepository<Stage> _stagesRepository;
        private IGenericRepository<TeamListTeam> _teamListTeamsRepository;
        public TournementService(IUnitOfWork unitOfWok,
                                IGenericRepository<PrizePool> prizePoolRepository,
                                IGenericRepository<Stage> stagesRepository,
                                IGenericRepository<Tournaments> tournementsRepository,
                                IGenericRepository<TeamListTeam> teamListTeamsRepository)
        {
            _tournamentsRepository = tournementsRepository;
            _unitOfWork = unitOfWok;
            _stagesRepository = stagesRepository;
            _prizePoolRepository = prizePoolRepository;
            _teamListTeamsRepository = teamListTeamsRepository;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public List<TournementsDto> GetAll()
        {
            List<TournementsDto> TournamentsDtosList = new List<TournementsDto>();
            try
            {
                List<Tournaments> TounamentsList = _tournamentsRepository.GetAll().
                    Include(t => t.Game).
                    Include(t => t.PrizePool).
                    ToList();
                foreach (var Tournament in TounamentsList)
                {
                    TournamentsDtosList.Add(new TournementsDto(Tournament));
                }
            }
            catch (Exception)
            { }
            return TournamentsDtosList;
        }

        public List<TournementsDto> GetGameTournements(int GameId)
        {
            List<TournementsDto> TournamentsDtoList = new List<TournementsDto>();
            try
            {
                List<Tournaments> TounamentsList = _tournamentsRepository.FindBy(T => T.GameId == GameId).
                    Include(t => t.Game).
                    Include(t => t.PrizePool).
                    Include(t => t.News).
                    ToList();
                foreach (var Tournament in TounamentsList)
                {
                    TournamentsDtoList.Add(new TournementsDto(Tournament));
                }
            }
            catch (Exception)
            { }
            return TournamentsDtoList;
        }

        private ICollection<Team> GetTeams(int TeamListId)
        {
            List<Team> Teams = new List<Team>();
            try
            {
                Teams = _teamListTeamsRepository.FindBy(x => x.TeamListId == TeamListId).
                    Include(x => x.Team).
                        ThenInclude(x => x.Players).
                    Include(x => x.Team).
                        ThenInclude(x => x.Country).
                    Select(x => x.Team).
                    OrderBy(x => x.Name).
                    ToList();
            } catch (Exception)
            { }
            return Teams;
        }

        public TournementsDto GetTournement(int Id)
        {
            try
            {
                Tournaments tournament = _tournamentsRepository.FindBy(T => T.Id == Id).
                    Include(t => t.Game).
                    Include(t => t.PrizePool).
                        ThenInclude(p => p.Distribution).
                            ThenInclude(d => d.Team).
                    Include(t => t.News).
                        ThenInclude(n => n.Author).
                    Include(t => t.Stages).
                        ThenInclude(s => s.Matches).
                            ThenInclude(m => m.TeamA).
                    Include(t => t.Stages).
                        ThenInclude(s => s.Matches).
                            ThenInclude(m => m.TeamB).
                    Include(t => t.TeamLists).
                    Include(t => t.Streams).
                        ThenInclude(s => s.Language).
                    FirstOrDefault();
                tournament.PrizePool.Distribution = tournament.PrizePool.Distribution.OrderBy(d => d.Place).ToList();
                for (int i = 0; i < tournament.TeamLists.Count; i++)
                {
                    tournament.TeamLists.ElementAt(i).Teams = GetTeams(tournament.TeamLists.ElementAt(i).Id);
                }
                TournementsDto TournamentsDto = new TournementsDto(tournament);
                return TournamentsDto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<TournementsDto> LatestTournements()
        {
            List<TournementsDto> TournamentsDto = new List<TournementsDto>();
            try
            {
                List<Tournaments> tournamentList = _tournamentsRepository.GetAll().
                    OrderBy(T => T.Id).
                    Take(1).
                    Include(t => t.Game).
                    Include(t => t.PrizePool).
                    ToList();
                foreach (var Tournament in tournamentList)
                {
                    TournamentsDto.Add(new TournementsDto(Tournament));
                }
            }
            catch (Exception)
            { }
            return TournamentsDto;
        }

        /*
        public List<PrizePoolDto> TournametPrizePool(int TournamentId)
        {
            List<PrizePoolDto> prizepooldtolist = new List<PrizePoolDto>();
            try
            {
                List<PrizePool> PrizePoolList = _prizePoolRepository.FindBy(P => P.TournamentId == TournamentId).ToList();
                foreach (var prizepool in PrizePoolList)
                {
                    prizepooldtolist.Add(new PrizePoolDto { Id = prizepool.Id, Level = prizepool.Level, Prize = prizepool.Prize, TeamId = prizepool.TeamId, TeamName = prizepool.Team.Name });
                }

            }
            catch (Exception)
            { }
            return prizepooldtolist;
        }
        */
        /*
        public List<StageDto> TournamentStages(int TournamentId)
        {
            List<StageDto> stagesDtoList = new List<StageDto>();
            try
            {
                List<Stage> stagesList = _stagesRepository.FindBy(g => g.TournamentId == TournamentId).
                    Include(s => s.TeamList).
                        ThenInclude(l => l.Team).
                    ToList();
                foreach (var stage in stagesList)
                {
                    stagesDtoList.Add(new StageDto(stage));
                }

            }
            catch (Exception)
            { }
            return stagesDtoList;
        }
        */
        public int GetLatestTornamentId()
        {
            try
            {
                Tournaments tournament = _tournamentsRepository.GetAll().OrderByDescending(T => T.Id).Take(1).FirstOrDefault();
                return tournament.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
