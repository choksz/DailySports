using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public long Phone { get; set; }
        public string Image { get; set; }
        public UserType Type { get; set; }
        public string SecurityCode { get; set; }
    }
}
