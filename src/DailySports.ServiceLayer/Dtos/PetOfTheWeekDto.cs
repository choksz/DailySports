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
        public Gender Gender { get; set; }
        public string Owner { get; set; }
        public string FunFact { get; set;}
        public int Age { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public PetOfTheWeekDto() { }

        public PetOfTheWeekDto(PetOfTheWeek pet)
        {
            Id = pet.Id;
            Title = pet.Title;
            Description = pet.Description;
            PetImage = pet.PetImage;
            Gender = pet.Gender;
            Owner = pet.Owner;
            FunFact = pet.FunFact;
            Age = pet.Age;
            StartDate = pet.StartDate;
            EndDate = pet.EndDate;
        }
    }
}
