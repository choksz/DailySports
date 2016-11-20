using System;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public  class PetOfTheWeekDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PetImage { get; set; }
        public GenderEnum Gender { get; set; }
        public string Owner { get; set; }
        public string FunFact { get; set;}
        public int Age { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
