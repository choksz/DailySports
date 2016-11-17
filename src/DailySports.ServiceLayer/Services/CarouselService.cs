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
    public class CarouselService : ICarouselService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<CarouselItem> _carouselRepository;

        public CarouselService(IUnitOfWork unitOfWork, IGenericRepository<CarouselItem> carouselRepository)
        {
            _unitOfWork = unitOfWork;
            _carouselRepository = carouselRepository;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public List<CarouselItemDto> GetAll()
        {
            List<CarouselItemDto> CarouselDtoList = new List<CarouselItemDto>();
            try
            {
                List<CarouselItem> CarouselList = _carouselRepository.GetAll().ToList();
                foreach (var item in CarouselList)
                {
                    CarouselDtoList.Add(new CarouselItemDto(item));
                }
                return CarouselDtoList;
            }
            catch (Exception)
            { }
            return CarouselDtoList;
        }
    }
}
