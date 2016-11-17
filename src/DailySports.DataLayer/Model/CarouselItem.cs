using System.ComponentModel.DataAnnotations;

namespace DailySports.DataLayer.Model
{
    public class CarouselItem
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
    }
}
