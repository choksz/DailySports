﻿using DailySports.ServiceLayer.IServices;
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
                List<Tournaments> TounamentsList = _tournamentsRepository.GetAll().ToList();
                foreach (var Tournament in TounamentsList)
                {
                    TournamentsDtosList.Add(new TournementsDto(Tournament));
                }
            }
            catch (Exception _)
            {
            }
            return TournamentsDtosList;
        }

        public List<TournementsDto> GetGameTournements(int GameId)
        {
            List<TournementsDto> TournamentsDtoList = new List<TournementsDto>();
            try
            {
                List<Tournaments> TounamentsList = _tournamentsRepository.FindBy(T => T.GameId == GameId).ToList();
                foreach (var Tournament in TounamentsList)
                {
                    TournamentsDtoList.Add(new TournementsDto(Tournament));
                }
            }
            catch (Exception _)
            {
            }
            return TournamentsDtoList;
        }

        public TournementsDto GetTournement(int Id)
        {
            try
            {
                Tournaments tournament = _tournamentsRepository.FindBy(T => T.Id == Id).FirstOrDefault();
                TournementsDto TournamentsDto = new TournementsDto(tournament);
                return TournamentsDto;

            }
            catch (Exception _)
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
                foreach (var tournament in tournamentList)
                {
                    TournamentsDto.Add(new TournementsDto(tournament));
                }
            }
            catch (Exception _)
            {
                return null;
            }
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
            catch (Exception _)
            {
            }
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
            catch (Exception _)
            {
            }
            return groupStegesDtoList;
        }

        public int GetLatestTornamentId()
        {
            try
            {
                Tournaments tournament = _tournamentsRepository.GetAll().OrderByDescending(T => T.Id).Take(1).FirstOrDefault();
                return tournament.Id;
            }
            catch (Exception _)
            {
                return 0;
            }
        }
    }
}
