using DailySports.ServiceLayer.Dtos;
using System;
using System.Collections.Generic;

namespace DailySports.ServiceLayer.IServices
{
    public interface ILatestService:IDisposable
    {
        List<EventDto> GetLatestEvents();
        List<NewsDto> GetLatestNews();
        List<VideoDto> GetLatestVideos();
        List<TournementsDto> GetLatestTournaments();
        List<TournementsDto> GetOngoingTournaments();
    }
}
