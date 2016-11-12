using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public  class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set;}
        public string Email { get; set; } 
        public string Biography { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string ConfirmPassWord { get; set; } 
        public bool rememberme { get; set; }
        public string SecurityCode { get; set; }
        public UserType Type;

        public UserDto() { }
        public UserDto(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Biography = user.Biography;
            Image = user.Image;
            Type = user.Type;
        }
    }
}
