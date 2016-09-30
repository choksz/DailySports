using DailySports.ServiceLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
