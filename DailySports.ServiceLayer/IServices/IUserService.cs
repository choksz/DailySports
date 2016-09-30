using DailySports.ServiceLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DailySports.ServiceLayer.IServices
{
    public interface IUserService : IDisposable
    {
        List<UserDto> GetAll();
        bool AddUser(UserDto user,HttpPostedFileBase file);
        UserDto Login(string Email, string Password);
        UserDto GetUserByEmail(string Email);
        bool ChangePassword(string Email, string Password);
        UserDto GetUser(string Email, string Code);
    }
}
