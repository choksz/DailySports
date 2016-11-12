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
    public class PetService : IPetService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<PetOfTheWeek> _petRepository;
        public PetService(IUnitOfWork unitOfWork,IGenericRepository<PetOfTheWeek> petRepository)
        {
            _unitOfWork = unitOfWork;
            _petRepository = petRepository;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public List<PetOfTheWeekDto> GetPetOfTheWeek()
        {
            try
            {
                DateTime startdate = DateTime.Today;
                int delta = DayOfWeek.Sunday - startdate.DayOfWeek;
                startdate = startdate.AddDays(delta);
                DateTime endDate = startdate.AddDays(7);
                List<PetOfTheWeek> petsList = _petRepository.GetAll().Where(P => P.StartDate>=startdate&&P.EndDate<endDate).OrderBy(P=>P.Id).Take(1).ToList();
                List<PetOfTheWeekDto> PetDto = new List<PetOfTheWeekDto>();
                foreach(var pet in petsList)
                {
                    PetDto.Add(new PetOfTheWeekDto
                    {
                        Id = pet.Id,
                        Title = pet.Title,
                        Description = pet.Description,
                        PetImage = pet.PetImage,
                        FunFact = pet.FunFact,
                        Gender = pet.Gender,
                        Owner = pet.Owner,
                        Age = pet.Age,
                        EndDate=pet.EndDate,
                         StartDate=pet.StartDate
                    });
                }
               return PetDto;
            }
            catch(Exception ex)
            {
                return new List<PetOfTheWeekDto>();
            }
        }
    }
}
