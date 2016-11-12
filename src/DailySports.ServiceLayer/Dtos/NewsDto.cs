using System;
using System.Collections.Generic;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public  class NewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public UserDto Author { get; set; }
        public string AuthorName { get; set; }
        public string AuthorBigraphy { get; set; }
        public string NewsImage { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public GameDto Game { get; set; }
        public List<PetOfTheWeekDto> PetOfTheDate { get; set; }
        public List<MatchDto> NextMatches { get; set; }

        public NewsDto() { }

        public NewsDto(News news)
        {
            Id = news.Id;
            Title = news.Title;
            Description = news.Description;
            CategoryId = news.CategoryId;
            CategoryName = news.category.Name;
            AuthorName = news.Author.Name;
            AuthorBigraphy = news.Author.Biography;
            Date = news.Date;
            NewsImage = news.NewsImage;
            Game = new GameDto(news.game);
            Author = new UserDto(news.Author);
        }
    }
}
