using System.ComponentModel.DataAnnotations;

namespace DailySports.DataLayer.Model
{

    public  class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
