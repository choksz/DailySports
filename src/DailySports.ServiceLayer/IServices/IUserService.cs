using DailySports.ServiceLayer.Dtos;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace DailySports.ServiceLayer.IServices
{
    public interface IUserService : IDisposable
    {
        List<UserDto> GetAll();
        bool AddUser(UserDto user, IFormFile file);
        UserDto Login(string Email, string Password);
        UserDto GetUserByEmail(string Email);
        bool ChangePassword(string Email, string Password);
        UserDto GetUser(string Email, string Code);
    }
}
