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

        public PetOfTheWeekDto GetPetOfTheWeek()
        {
            try
            {
                DateTime today = DateTime.Today;
                PetOfTheWeek pet = _petRepository.FindBy(p => p.StartDate <= today && p.EndDate >= today).First();
                PetOfTheWeekDto PetDto = new PetOfTheWeekDto(pet);
                return PetDto;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
