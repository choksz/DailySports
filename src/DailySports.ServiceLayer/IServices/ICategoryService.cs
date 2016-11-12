using DailySports.ServiceLayer.Dtos;
using System;
using System.Collections.Generic;

namespace DailySports.ServiceLayer.IServices
{
    public interface ICategoryService:IDisposable
    {
        List<CategoryDto> GetAll();
    }
}
