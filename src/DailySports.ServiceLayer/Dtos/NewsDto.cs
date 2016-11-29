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
        public string NewsImage { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public GameDto Game { get; set; }
        public PetOfTheWeekDto PetOfTheWeek { get; set; }

        public NewsDto() { }

        public NewsDto(News news)
        {
            Id = news.Id;
            Title = news.Title;
            Description = news.Description;
            CategoryId = news.CategoryId;
            Category= (news.category != null) ? new CategoryDto(news.category) : null;
            Date = news.Date;
            NewsImage = news.NewsImage;
            Game = (news.game != null) ? new GameDto(news.game) : null;
            Author = (news.Author != null) ? new UserDto(news.Author) : null;
        }
    }
}
