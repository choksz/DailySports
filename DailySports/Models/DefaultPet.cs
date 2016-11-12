using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DailySports.DataLayer.Model;
namespace DailySports.Models
{
    public class DefaultPet
    {
        public int Id = 1;
        public string Name = "Cat";
        public int Age = 6;
        public string Description = "Lorem Ipsum is simply dummy";
        public GenderEnum Gender = GenderEnum.Female;
        public string Owner = "Freya_Chan – Cosplayer and Partnered Dota 2 Streamer";
        public string Image = "cat_image.jpg";
        public string FunFact = "She is a very motherly, lively and social little girl, who loves Tuna and likes to play with water.";
    }
}