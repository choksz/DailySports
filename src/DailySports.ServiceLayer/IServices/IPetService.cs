using DailySports.ServiceLayer.Dtos;
using System;

namespace DailySports.ServiceLayer.IServices
{
    public interface IPetService:IDisposable
    {
        PetOfTheWeekDto GetPetOfTheWeek();
    }
}
