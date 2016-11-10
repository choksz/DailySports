using DailySports.ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailySports.ServiceLayer.Dtos;
using DailySports.DataLayer.Model;
using DailySports.ServiceLayer.UnitOfWork;
using DailySports.ServiceLayer.Repositories.Core;
using System.Web;
using System.IO;

namespace DailySports.ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private IGenericRepository<User> _userRepository;

        public UserService(IUnitOfWork unitOfWork, IGenericRepository<User> userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;

        }

        public bool AddUser(UserDto user, HttpPostedFileBase file)
        {
            try
            {
                User newUser = new User();
                newUser.Name = user.Name;
                newUser.Password = user.Password;
                newUser.Email = user.Email;
                newUser.Biography = user.Biography;
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    string generatedName = Guid.NewGuid().ToString() + fileName;
                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Admin/Users"), generatedName);
                    newUser.Image = fileName;
                    file.SaveAs(path);
                }
                newUser.Type = UserType.SiteUser;
                newUser.SecurityCode = user.SecurityCode;
                _userRepository.Add(newUser);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ChangePassword(string Email, string Password)
        {
            try
            {
                User user = _userRepository.FindBy(U => U.Email == Email).FirstOrDefault();
                user.Password = Password;
                _userRepository.Update(user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public List<UserDto> GetAll()
        {
            try
            {
                List<User> UsersList = _userRepository.GetAll().ToList();
                List<UserDto> UserDtoList = new List<UserDto>();
                foreach (var user in UsersList)
                {
                    UserDtoList.Add(new UserDto { Id = user.Id, Name = user.Name, Email = user.Email, Password = user.Password });
                }
                return UserDtoList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserDto GetUserByEmail(string Email)
        {
            try
            {
                User user = _userRepository.FindBy(U => U.Email == Email).FirstOrDefault();
                UserDto newUser = new UserDto();
                newUser.Id = user.Id;
                newUser.Name = user.Name;
                newUser.Password = user.Password;
                newUser.Image = user.Image;
                newUser.Biography = user.Biography;
                newUser.Email = user.Email;
                return newUser;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserDto Login(string Email, string Password)
        {
            try
            {
                User newUser = _userRepository.FindBy(U => U.Email == Email && U.Password == Password).FirstOrDefault();
                UserDto user = new UserDto();
                user.Id = newUser.Id;
                user.Name = newUser.Name;
                user.Email = newUser.Email;
                user.Biography = newUser.Biography;
                user.Image = newUser.Image;
                user.Password = newUser.Password;
                user.Type = newUser.Type;
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UserDto GetUser(string Email, string SecurityCode)
        {
            try
            {
                User user = _userRepository.FindBy(U => U.Email == Email && U.SecurityCode == SecurityCode).FirstOrDefault();
                UserDto userDto = new UserDto();
                userDto.Id = user.Id;
                userDto.Name = user.Name;
                return userDto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
