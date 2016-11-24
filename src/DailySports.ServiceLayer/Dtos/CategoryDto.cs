using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public  class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryDto() { }
        public CategoryDto(Category cat)
        {
            Id = cat.Id;
            Name = cat.Name;
        }
    }
}
