using DailySports.ServiceLayer.Dtos;
using System;
using System.Collections.Generic;

namespace DailySports.ServiceLayer.IServices
{
    public interface IPetService:IDisposable
    {
        List<PetOfTheWeekDto> GetPetOfTheWeek();
    }
}
