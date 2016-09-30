using DailySports.ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailySports.ServiceLayer.Dtos;
using DailySports.ServiceLayer.UnitOfWork;
using DailySports.ServiceLayer.Repositories.Core;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<Category> _categoryRepository;
        public CategoryService(IUnitOfWork unitOfWork,IGenericRepository<Category> categoryRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public List<CategoryDto> GetAll()
        {
            try
            {
                List<Category> CategoryList = _categoryRepository.GetAll().ToList();
                List<CategoryDto> CategoryDtoList = new List<CategoryDto>();
                foreach(var category in CategoryList)
                {
                    CategoryDtoList.Add(new CategoryDto {Id=category.Id,Name=category.Name });
                }
                return CategoryDtoList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
