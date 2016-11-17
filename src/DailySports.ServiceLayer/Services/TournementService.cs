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
        private IGenericRepository<GroupStages> _groupStageRepository;
        public TournementService(IUnitOfWork unitOfWok, IGenericRepository<PrizePool> prizePoolRepository, IGenericRepository<GroupStages> groupStageRepository, IGenericRepository<Tournaments> tournementsRepository)
        {
            _tournamentsRepository = tournementsRepository;
            _unitOfWork = unitOfWok;
            _groupStageRepository = groupStageRepository;
            _prizePoolRepository = prizePoolRepository;
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
                List<Tournaments> TounamentsList = _tournamentsRepository.GetAll().Include(t => t.Game).ToList();
                foreach (var Tournament in TounamentsList)
                {
                    TournamentsDtosList.Add(new TournementsDto
                    {
                        Id = Tournament.Id,
                        Title = Tournament.Title,
                        Description = Tournament.Description,
                        Format = Tournament.Format,
                        MainEvent = Tournament.MainEvent,
                        GameId = Tournament.GameId,
                        Game = new GameDto(Tournament.Game),
                        Overview = Tournament.Overview,
                        TournamentImage = Tournament.TournamentImage,
                        StartDate = Tournament.StartDate,
                        EndDate = Tournament.EndDate,
                        URL = Tournament.URL,
                        Price = Tournament.Price,
                        Qualifiers = Tournament.Qualifiers
                    }
                    );
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
                    Include(t => t.Matches).
                    Include(t => t.News).
                    Include(t => t.Videos).ToList();
                foreach (var Tournament in TounamentsList)
                {
                    TournamentsDtoList.Add(new TournementsDto(Tournament));
                }
            }
            catch (Exception)
            { }
            return TournamentsDtoList;
        }

        public TournementsDto GetTournement(int Id)
        {
            try
            {
                Tournaments tournament = _tournamentsRepository.FindBy(T => T.Id == Id).
                    Include(t => t.Game).
                    Include(t => t.PrizePool).
                    Include(t => t.Matches).
                        ThenInclude(m => m.TeamA).
                    Include(t => t.Matches).
                        ThenInclude(m => m.TeamB).
                    Include(t => t.News).
                    Include(t => t.Videos).
                    Include(t => t.GroupStages).FirstOrDefault();
                TournementsDto TournamentsDto = new TournementsDto
                {
                    Id = tournament.Id,
                    Title = tournament.Title,
                    Description = tournament.Description,
                    Format = tournament.Format,
                    MainEvent = tournament.MainEvent,
                    GameId = tournament.GameId,
                    Overview = tournament.Overview,
                    Price = tournament.Price,
                    Qualifiers = tournament.Qualifiers,
                    StartDate = tournament.StartDate,
                    EndDate = tournament.EndDate,
                    URL = tournament.URL,
                    TournamentImage = tournament.TournamentImage,
                    Game = new GameDto(tournament.Game),
                    TournamentMatches = new List<MatchDto>(),
                    TournamentPrizePool = new List<PrizePoolDto>(),
                    TournamentGroupStages = new List<GroupStagesDto>(),
                    NextMatches = new List<MatchDto>()
                    /*if (tournament.Matches != null)
                    {
                        foreach (var match in tournament.Matches)
                        {
                            TournamentMatches.Add(new MatchDto(match));
                        }
                    }

                    if (tournament.PrizePool != null)
                    {
                        foreach (var prize in tournament.PrizePool)
                        {
                            TournamentPrizePool.Add(new PrizePoolDto(prize));
                        }
                    }

                    if (tournament.GroupStages != null)
                    {
                        foreach (var groupStage in tournament.GroupStages)
                        {
                            TournamentGroupStages.Add(new GroupStagesDto(groupStage));
                        }
                    }*/

                };
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
                List<Tournaments> tournamentList = _tournamentsRepository.GetAll().OrderBy(T => T.Id).Take(1).ToList();
                foreach (var Tournament in tournamentList)
                {
                    TournamentsDto.Add(new TournementsDto
                    {
                        Id = Tournament.Id,
                        Title = Tournament.Title,
                        Description = Tournament.Description,
                        Format = Tournament.Format,
                        MainEvent = Tournament.MainEvent,
                        GameId = Tournament.GameId,
                        Game = new GameDto(Tournament.Game),
                        Overview = Tournament.Overview,
                        TournamentImage = Tournament.TournamentImage,
                        StartDate = Tournament.StartDate,
                        EndDate = Tournament.EndDate,
                        URL = Tournament.URL,
                        Price = Tournament.Price,
                        Qualifiers = Tournament.Qualifiers
                    }
                    );
                }
            }
            catch (Exception)
            { }
            return TournamentsDto;
        }

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

        public List<GroupStagesDto> TournamentGroupStages(int TournamentId)
        {
            List<GroupStagesDto> groupStegesDtoList = new List<GroupStagesDto>();
            try
            {
                List<GroupStages> groupStagesList = _groupStageRepository.FindBy(g => g.TournamentId == TournamentId).ToList();
                List<TeamDto> teamdtolist = new List<TeamDto>();
                foreach (var groupstage in groupStagesList)
                {
                    foreach (var team in groupstage.Team)
                    {
                        teamdtolist.Add(new TeamDto { Id = team.Id, Name = team.Name });
                    }
                    groupStegesDtoList.Add(new GroupStagesDto { Id = groupstage.Id, TeamList = teamdtolist });
                }

            }
            catch (Exception)
            { }
            return groupStegesDtoList;
        }

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
