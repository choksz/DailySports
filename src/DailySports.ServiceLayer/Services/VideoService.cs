using DailySports.ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using DailySports.ServiceLayer.Dtos;
using DailySports.ServiceLayer.UnitOfWork;
using DailySports.DataLayer.Model;
using DailySports.ServiceLayer.Repositories.Core;

namespace DailySports.ServiceLayer.Services
{
    public class VideoService : IVideoService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Videos> _videoRepository;
        public VideoService(IUnitOfWork unitOfWork,IGenericRepository<Videos> videoRepository)
        {
            _unitOfWork = unitOfWork;
            _videoRepository = videoRepository;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public List<VideoDto> GetAll()
        {
            try
            {
                List<Videos> VideosList = _videoRepository.GetAll().ToList();
                List<VideoDto> VideoDtoList = new List<VideoDto>();
                foreach(var video in VideosList)
                {
                    VideoDtoList.Add(new VideoDto {Id=video.Id,TournamentID=video.TournamentId,Description=video.Description,GameId=video.GameId,Title=video.Title,URL=video.Url });
                }
                return VideoDtoList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<VideoDto> GetCategoryVideos(int CategoryId)
        {
            try
            {
                List<Videos> VideoList = _videoRepository.FindBy(V => V.CategoryId == CategoryId).ToList();
                List<VideoDto> VideoDtoList = new List<VideoDto>();
                foreach (var video in VideoList)
                {
                    VideoDtoList.Add(new VideoDto { Id = video.Id, TournamentID = video.TournamentId, Description = video.Description, GameId = video.GameId, Title = video.Title, URL = video.Url });
                }
                return VideoDtoList;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<VideoDto> GetGamesVideos(int GameId)
        {
            try
            {
                List<Videos> VideoList = _videoRepository.FindBy(V => V.GameId == GameId).ToList();
                List<VideoDto> VideoDtoList = new List<VideoDto>();
                foreach (var video in VideoList)
                {
                    VideoDtoList.Add(new VideoDto { Id = video.Id,TournamentID=video.TournamentId ,Description = video.Description, GameId = video.GameId, Title = video.Title, URL = video.Url });
                }
                return VideoDtoList;

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<VideoDto> GetLastWeekVideos()
        {
            try
            {
                List<Videos> VideosList = _videoRepository.FindBy(V => V.Date.DayOfYear == DateTime.Now.DayOfYear - 8).ToList();
                List<VideoDto> videoDtoList = new List<VideoDto>();
                foreach(var video in VideosList)
                {
                    videoDtoList.Add(new VideoDto {Id=video.Id,TournamentID=video.TournamentId,Title=video.Title,Description=video.Description,GameId=video.GameId,URL=video.Url });
                }
                return videoDtoList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public VideoDto GetVideo(int id)
        {
          try
            {
                Videos video = _videoRepository.FindBy(V => V.Id == id).FirstOrDefault();
                VideoDto videoDto = new VideoDto();
                videoDto.Id = video.Id;
                videoDto.Title = video.Title;
                videoDto.Description = video.Description;
                videoDto.GameId = video.GameId;
                videoDto.URL = video.Url;
                videoDto.TournamentID = video.TournamentId;
                return videoDto;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<VideoDto> Search(string search)
        {
            try
            {
                List<Videos> VideoList = _videoRepository.FindBy(V => V.Title.Contains(search)).ToList();
                List<VideoDto> VideoDtoList = new List<VideoDto>();
                foreach (var video in VideoList)
                {
                    VideoDtoList.Add(new VideoDto { Id = video.Id, TournamentID = video.TournamentId, Description = video.Description, GameId = video.GameId, Title = video.Title, URL = video.Url });
                }
                return VideoDtoList;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
