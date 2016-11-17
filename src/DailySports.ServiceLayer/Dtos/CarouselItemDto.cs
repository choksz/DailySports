using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class CarouselItemDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }

        public CarouselItemDto() { }
        public CarouselItemDto(CarouselItem item)
        {
            Id = item.Id;
            Image = item.Image;
            Text = item.Text;
        }
    }
}
