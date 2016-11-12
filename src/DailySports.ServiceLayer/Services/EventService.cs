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
    public class EventService : IEventService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Event> _eventRepository;
        public EventService(IUnitOfWork unitOfWork,IGenericRepository<Event> eventRepository)
        {
            _unitOfWork = unitOfWork;
            _eventRepository = eventRepository;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public List<EventDto> GetAll()
        {
            try
            {
                List<Event> EventList = _eventRepository.GetAll().ToList();
                List<EventDto> EventDtoList = new List<EventDto>();
                List<EventImageDto> eventImagesList = new List<EventImageDto>();
                foreach(var Event in EventList)
                {
                    
                    foreach (var image in Event.Images)
                    {
                        eventImagesList.Add(new EventImageDto {Id=image.Id,File=image.File,EventId=image.EventId,Tag=image.Tag });
                    }
                    EventDtoList.Add(new EventDto
                    {
                        Id = Event.Id,
                        Title = Event.Title,
                        Description = Event.Description,
                        Location = Event.Location,
                        StartDate = Event.StartDate,
                        EndDate = Event.EndDate,
                        EventImage = Event.EventImage,
                        EventImages=eventImagesList

                    });
                }
                return EventDtoList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public EventDto GetEvent(int id)
        {
            try
            {
                Event newEvent = _eventRepository.FindBy(E => E.Id == id).FirstOrDefault();
                EventDto newEventDto = new EventDto();
                List<EventImageDto> ImageList = new List<EventImageDto>();
                newEventDto.Id = newEvent.Id;
                newEventDto.Title = newEvent.Title;
                newEventDto.Description = newEvent.Description;
                newEventDto.Location = newEvent.Location;
                newEventDto.StartDate = newEvent.StartDate;
                newEventDto.EndDate = newEvent.EndDate;
                newEventDto.EventImage = newEvent.EventImage;
                newEventDto.Currency = newEvent.Currency;
                newEventDto.Price = newEvent.Price;
                newEventDto.Tickets =int.Parse( newEvent.ticket.Quantity.ToString());
                if (newEvent.Images== null || newEvent.Images.Count == 0)
                {
                    newEventDto.EventImages = null;
                }
                else
                {
                    foreach (var image in newEvent.Images)
                    {
                        EventImageDto eventImage = new EventImageDto();
                        eventImage.Id = image.Id;
                        eventImage.Tag = image.Tag;
                        eventImage.File = image.File;
                        eventImage.EventId = image.EventId;
                        ImageList.Add(eventImage);
                      
                    }
                }
                newEventDto.EventImages = ImageList;
                return newEventDto;


            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public List<EventDto> SearchEvent(string Name)
        {
            try
            {
                List<Event> EvenList = _eventRepository.FindBy(E => E.Title.Contains(Name)).ToList();
                List<EventDto> EventDtoList = new List<EventDto>();
                foreach(var Event in EvenList)
                {
                    EventDtoList.Add(new EventDto {Id=Event.Id,Title=Event.Title,Description=Event.Description, StartDate=Event.StartDate,EndDate=Event.EndDate,Location=Event.Location,EventImage=Event.EventImage });
                }
                return EventDtoList;

            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public EventDto LatestEvent()
        {
            try
            {
                Event LatestEvent = _eventRepository.GetAll().OrderBy(E => E.Id).Take(1).FirstOrDefault();
                EventDto eventDto = new EventDto();
                eventDto.Id = LatestEvent.Id;
                eventDto.Title = LatestEvent.Title;
                eventDto.Description = LatestEvent.Description;
                eventDto.Location = LatestEvent.Location;
                eventDto.StartDate = LatestEvent.StartDate;
                eventDto.EndDate = LatestEvent.EndDate;
                eventDto.EventImage = LatestEvent.EventImage;
                if (LatestEvent.Images != null || LatestEvent.Images.Count != 0)
                {
                    foreach (var image in LatestEvent.Images)
                    {
                        eventDto.EventImages.Add(new EventImageDto { Id = image.Id, EventId = image.EventId, File = image.File, Tag = image.Tag });
                    }
                }
                else
                {
                    eventDto.EventImages = null;
                }
                return eventDto;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
