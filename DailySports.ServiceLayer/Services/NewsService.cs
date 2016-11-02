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
    public class NewsService : INewsService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<News> _newsRepository;
        public NewsService(IUnitOfWork unitOfWork, IGenericRepository<News> newsRepository)
        {
            _unitOfWork = unitOfWork;
            _newsRepository = newsRepository;

        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public List<NewsDto> GetAll()
        {
            try
            {
                List<News> NewsList = _newsRepository.GetAll().ToList();
                List<NewsDto> NewsDtoList = new List<NewsDto>();
                foreach (var news in NewsList)
                {
                    NewsDtoList.Add(new NewsDto(news));
                }
                return NewsDtoList;
            }
            catch (Exception _)
            {
                return null;
            }
        }

        public NewsDto GetNews(int id)
        {
            try
            {
                News newNews = _newsRepository.FindBy(N => N.Id == id).FirstOrDefault();
                NewsDto newsDto = new NewsDto(newNews);
                return newsDto;
            }
            catch (Exception _)
            {
                return null;
            }
        }

        public List<NewsDto> SearchNews(string Name)
        {
            try
            {
                List<News> NewsList = _newsRepository.FindBy(N => N.Title.Contains(Name)).ToList();
                List<NewsDto> NewsDtoList = new List<NewsDto>();
                foreach (var news in NewsList)
                {
                    NewsDtoList.Add(new NewsDto(news));
                }
                return NewsDtoList;
            }
            catch (Exception _)
            {
                return null;
            }
        }

        public List<NewsDto> LatestNews()
        {
            try
            {
                List<News> LatestNewsList = _newsRepository.GetAll().OrderBy(N => N.Id).Take(1).ToList();
                List<NewsDto> newsDto = new List<NewsDto>();
                foreach (var LatestNews in LatestNewsList)
                {
                    newsDto.Add(new NewsDto(LatestNews));
                }
                return newsDto;
            }
            catch (Exception _)
            {
                return null;
            }
        }
    }
}
