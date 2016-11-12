using DailySports.ServiceLayer.Dtos;
using System;
using System.Collections.Generic;

namespace DailySports.ServiceLayer.IServices
{
    public  interface IEventService:IDisposable
    {
        List<EventDto> GetAll();
        EventDto GetEvent(int id);
        List<EventDto> SearchEvent(string Name);
        EventDto LatestEvent();
    }
}
