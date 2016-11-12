using DailySports.ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
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
            List<NewsDto> NewsDtoList = new List<NewsDto>();
            try
            {
                List<News> NewsList = _newsRepository.GetAll().ToList();
                
                foreach (var news in NewsList)
                {
                    NewsDtoList.Add(new NewsDto(news));
                }
            }
            catch (Exception e)
            { }
            return NewsDtoList;
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
            List<NewsDto> NewsDtoList = new List<NewsDto>();
            try
            {
                List<News> NewsList = _newsRepository.FindBy(N => N.Title.Contains(Name)).ToList(); 
                foreach (var news in NewsList)
                {
                    NewsDtoList.Add(new NewsDto(news));
                }
            }
            catch (Exception _)
            {}
            return NewsDtoList;
        }

        public List<NewsDto> LatestNews()
        {
            List<NewsDto> newsDto = new List<NewsDto>();
            try
            {
                List<News> LatestNewsList = _newsRepository.GetAll().OrderBy(N => N.Id).Take(1).ToList();
                
                foreach (var LatestNews in LatestNewsList)
                {
                    newsDto.Add(new NewsDto(LatestNews));
                }
            }
            catch (Exception _)
            { }
            return newsDto;
        }
    }
}
