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
    public class LatestService : ILatestService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Event> _eventRepository;
        private IGenericRepository<Game> _gameRepository;
        private IGenericRepository<News> _newsRepository;
        private IGenericRepository<Tournaments> _tournamentsRepository;
        private IGenericRepository<Videos> _videoRepository;
        public LatestService(IUnitOfWork unitOfWork,IGenericRepository<Event> eventRepository,IGenericRepository<News> newsRepository,IGenericRepository<Tournaments> tournamentsRepository,IGenericRepository<Videos> videoRepository, IGenericRepository<Game> gameRepository)
        {
            _unitOfWork = unitOfWork;
            _newsRepository = newsRepository;
            _eventRepository = eventRepository;
            _gameRepository = gameRepository;
            _tournamentsRepository = tournamentsRepository;
            _videoRepository = videoRepository;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public List<EventDto> GetLatestEvents()
        {
            try
            {
                List<Event> eventList = _eventRepository.GetAll().OrderByDescending(E => E.StartDate).Take(4).ToList();
                List<EventDto> LatestEvents = new List<EventDto>();
                foreach(var Event in eventList)
                {
                    LatestEvents.Add(new EventDto
                    {
                        Id = Event.Id,
                        Title = Event.Title,
                        Description=Event.Description,
                        Location=Event.Location,
                        EventImage= Event.EventImage,
                        StartDate=Event.StartDate,
                        EndDate=Event.EndDate
                     });
                }
                return LatestEvents;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<NewsDto> GetLatestNews()
        {
            try
            {
                List<News> NewsList = _newsRepository.GetAll().OrderByDescending(N => N.Id).Take(4).ToList();
                List<NewsDto> LatestNews = new List<NewsDto>();
                Dictionary<int, Game> games = _gameRepository.GetAll().ToDictionary(g => g.Id);
                foreach(var news in NewsList)
                {
                    var game = games[news.GameId];
                    LatestNews.Add(new NewsDto
                    {
                        Title=news.Title,
                        Description=news.Description,
                        Id =news.Id,
                        AuthorName =news.Author.Name,
                        Date=news.Date,
                        NewsImage=news.NewsImage,
                        Game = new GameDto
                        {
                            Id = game.Id,
                            Name = game.Name,
                            GameImage = game.GameImage,
                            LiveStreamURL = game.LiveStreamUrl
                        }
                    });
                }
                return LatestNews;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<TournementsDto> GetLatestTournaments()
        {
            try
            {
                List<Tournaments> TournamentList = _tournamentsRepository.GetAll().OrderByDescending(T => T.StartDate).Take(4).ToList();
                List<TournementsDto> LatestTournaments = new List<TournementsDto>();
                foreach(var tournament in TournamentList)
                {
                    LatestTournaments.Add(new TournementsDto(tournament));
                }
                return LatestTournaments;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<VideoDto> GetLatestVideos()
        {
            try
            {
                List<Videos> VideoList = _videoRepository.GetAll().OrderByDescending(V => V.Date).Take(4).ToList();
                List<VideoDto> LatestVideos = new List<VideoDto>();
                foreach(var video in VideoList)
                {
                    LatestVideos.Add(new VideoDto
                    {
                        Id=video.Id,
                        Title=video.Title,
                        Description=video.Description,
                        GameId=video.GameId,
                        URL=video.Url
                    });
                }
                return LatestVideos;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<TournementsDto> GetOngoingTournaments()
        {
            List<TournementsDto> ongoingTournaments = new List<TournementsDto>();
            try
            {
                DateTime today = DateTime.Today;
                List<Tournaments> tournamentList = _tournamentsRepository.FindBy(T => T.StartDate <= today && T.EndDate >= today).ToList();
                foreach (var tournament in tournamentList)
                {
                    ongoingTournaments.Add(new TournementsDto(tournament));
                }
            }
            catch (Exception _)
            {
            }
            return ongoingTournaments;
        }
    }
}
