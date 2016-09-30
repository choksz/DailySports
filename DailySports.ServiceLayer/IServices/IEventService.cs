using DailySports.ServiceLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
