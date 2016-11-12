using System.ComponentModel.DataAnnotations;

namespace DailySports.DataLayer.Model
{
    public  class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Biography { get; set; }
        public string Image { get; set; }
        public UserType Type { get; set; }
        public string SecurityCode { get; set; }
    }
}
