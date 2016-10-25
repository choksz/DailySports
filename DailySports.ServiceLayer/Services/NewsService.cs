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
                    NewsDtoList.Add(new NewsDto
                    {
                        Id = news.Id,
                        Title = news.Title,
                        Description = news.Description,
                        CategoryId = news.CategoryId,
                        CategoryName = news.category.Name,
                        AuthorName = news.Author.Name,
                        AuthorBigraphy = news.Author.Biography,
                        Date = news.Date,
                        NewsImage = news.NewsImage
                    });
                }
                return NewsDtoList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public NewsDto GetNews(int id)
        {
            try
            {
                News newNews = _newsRepository.FindBy(N => N.Id == id).FirstOrDefault();
                NewsDto newsDto = new NewsDto();
                newsDto.Id = newNews.Id;
                newsDto.Title = newNews.Title;
                newsDto.Description = newNews.Description;
                newsDto.AuthorName = newNews.Author.Name;
                newsDto.AuthorBigraphy = newNews.Author.Biography;
                newsDto.Date = newNews.Date;
                newsDto.NewsImage = newNews.NewsImage;
                return newsDto;
            }
            catch (Exception ex)
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
                    NewsDtoList.Add(new NewsDto
                    {
                        Id = news.Id,
                        Title = news.Title,
                        Description = news.Description,
                        AuthorName = news.Author.Name,
                        CategoryId = news.CategoryId,
                        Date = news.Date,
                        NewsImage = news.NewsImage
                    });
                }
                return NewsDtoList;
            }
            catch (Exception ex)
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
                    newsDto.Add(new NewsDto
                    {
                        Id = LatestNews.Id,
                        Title = LatestNews.Title,
                        Description = LatestNews.Description,
                        CategoryId = LatestNews.CategoryId,
                        CategoryName = LatestNews.category.Name,
                        AuthorName = LatestNews.Author.Name,
                        AuthorBigraphy = LatestNews.Author.Biography,
                        Date = LatestNews.Date,
                        NewsImage = LatestNews.NewsImage
                    });
                }
                return newsDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
