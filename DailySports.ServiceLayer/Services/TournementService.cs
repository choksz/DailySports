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
    public class TournementService : ITournementsService
    {
        Tournaments abc = new Tournaments();
        TournementsDto abc2 = new TournementsDto();
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Tournaments> _tournamentsRepository;
        private IGenericRepository<PrizePool> _prizePoolRepository;
        private IGenericRepository<GroupStages> _groupStageRepository;
        public TournementService(IUnitOfWork unitOfWok,IGenericRepository<PrizePool> prizePoolRepository,IGenericRepository<GroupStages> groupStageRepository,IGenericRepository<Tournaments> tournementsRepository)
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
            try
            { 
                List<Tournaments> TounamentsList = _tournamentsRepository.GetAll().ToList();
                List<TournementsDto> TournamentsDtoList = new List<TournementsDto>();
                using (Tournaments abcx = new Tournaments()) {
                    foreach (var Tournament in TounamentsList)
                    {
                        TournamentsDtoList.Add(new TournementsDto
                        {
                            Id = Tournament.Id,
                            Description = Tournament.Description,
                            Title = Tournament.Title,
                            Format = Tournament.Format,
                            MainEvent = Tournament.MainEvent,
                            Overview = Tournament.Overview,
                            Qualifiers = Tournament.Qualifiers,
                            EndDate = Tournament.EndDate,
                            Price = Tournament.Price,
                            StartDate = Tournament.StartDate,
                            URL = Tournament.URL,
                            GameId = Tournament.GameId,
                            TournamentImage = Tournament.TournamentImage
                        });
                    }
                }
                return TournamentsDtoList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<TournementsDto> GetGameTournements(int GameId)
        {
            try
            {
                List<Tournaments> TounamentsList = _tournamentsRepository.FindBy(T=>T.GameId==GameId).ToList();
                List<TournementsDto> TournamentsDtoList = new List<TournementsDto>();
                foreach (var Tournament in TounamentsList)
                {
                    TournamentsDtoList.Add(new TournementsDto
                    {
                        Id = Tournament.Id,
                        Description = Tournament.Description,
                        Title = Tournament.Title,
                        Format = Tournament.Format,
                        MainEvent = Tournament.MainEvent,
                        Overview = Tournament.Overview,
                        Qualifiers = Tournament.Qualifiers,
                        EndDate = Tournament.EndDate,
                        Price = Tournament.Price,
                        StartDate = Tournament.StartDate,
                        URL = Tournament.URL,
                        GameId=Tournament.GameId,
                        TournamentImage=Tournament.TournamentImage
                    });
                }
                return TournamentsDtoList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public TournementsDto GetTournement(int Id)
        {
            try
            {
                Tournaments tournament = _tournamentsRepository.FindBy(T => T.Id == Id).FirstOrDefault();
                TournementsDto TournamentsDto = new TournementsDto();
                TournamentsDto.Id = tournament.Id;
                TournamentsDto.Title = tournament.Title;
                TournamentsDto.Description = tournament.Description;
                TournamentsDto.Format = tournament.Format;
                TournamentsDto.MainEvent = tournament.MainEvent;
                TournamentsDto.Overview = tournament.Overview;
                TournamentsDto.Qualifiers = tournament.Qualifiers;
                TournamentsDto.Price = tournament.Price;
                TournamentsDto.StartDate = tournament.StartDate;
                TournamentsDto.EndDate = tournament.EndDate;
                TournamentsDto.GameId = tournament.GameId;
                TournamentsDto.URL = tournament.URL;
                TournamentsDto.TournamentImage = tournament.TournamentImage;
                return TournamentsDto;

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<TournementsDto> LatestTournements()
        {
           try
            {
                List<Tournaments> tournamentList = _tournamentsRepository.GetAll().OrderBy(T => T.Id).Take(1).ToList();
                List<TournementsDto> TournamentsDto = new List<TournementsDto>();
                foreach(var tournament in tournamentList)
                {
                    TournamentsDto.Add(new TournementsDto { Id = tournament.Id,
                        Title = tournament.Title,
                        Description = tournament.Description,
                        Format = tournament.Format,
                        MainEvent = tournament.MainEvent,
                        Overview = tournament.Overview,
                        Qualifiers = tournament.Qualifiers,
                        Price = tournament.Price,
                        StartDate = tournament.StartDate,
                        EndDate = tournament.EndDate,
                        GameId = tournament.GameId,
                        URL = tournament.URL,
                        TournamentImage=tournament.TournamentImage
                    });
                }
               
                return TournamentsDto;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<PrizePoolDto> TournametPrizePool(int TournamentId)
        {
           try
            {
                List<PrizePool> PrizePoolList = _prizePoolRepository.FindBy(P => P.TournamentId == TournamentId).ToList();
                List<PrizePoolDto> prizepooldtolist = new List<PrizePoolDto>();
                foreach(var prizepool in PrizePoolList)
                {
                    prizepooldtolist.Add(new PrizePoolDto {Id=prizepool.Id,Level=prizepool.Level, Prize=prizepool.Prize,TeamId=prizepool.TeamId,TeamName=prizepool.Team.Name });
                }
                return prizepooldtolist;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<GroupStagesDto> TournamentGroupStages(int TournamentId)
        {
            try
            {
                List<GroupStages> groupStagesList = _groupStageRepository.FindBy(g => g.TournamentId == TournamentId).ToList();
                List<GroupStagesDto> groupStegesDtoList = new List<GroupStagesDto>();
                List<TeamDto> teamdtolist = new List<TeamDto>();
                foreach (var groupstage in groupStagesList)
                {
                    foreach (var team in groupstage.Team)
                    {
                        teamdtolist.Add(new TeamDto { Id = team.Id, Name = team.Name });
                    }
                    groupStegesDtoList.Add(new GroupStagesDto {Id=groupstage.Id,TeamList=teamdtolist });
                }
                return groupStegesDtoList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public int GetLatestTornamentId()
        {
            try
            {
                Tournaments tournament = _tournamentsRepository.GetAll().OrderByDescending(T => T.Id).Take(1).FirstOrDefault();
                return tournament.Id;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        
    }
}
