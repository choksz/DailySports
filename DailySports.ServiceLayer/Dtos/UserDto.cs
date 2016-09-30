using DailySports.DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Dtos
{
  public  class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set;}
        public string Email { get; set; } 
        public string Biography { get; set; }
        public long Phone { get; set; }
        public string Image { get; set; }
        public UserType Type { get; set; }
        public string ConfirmPassWord { get; set; } 
        public bool rememberme { get; set; }
       public string SecurityCode { get; set; }
    }
}
