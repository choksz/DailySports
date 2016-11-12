using DailySports.ServiceLayer.Dtos;
using System;
using System.Collections.Generic;

namespace DailySports.ServiceLayer.IServices
{
    public interface INewsService :IDisposable
    {
        List<NewsDto> GetAll();
        NewsDto GetNews(int id);
        List<NewsDto> SearchNews(string Name);
        List<NewsDto> LatestNews();
    }
}
