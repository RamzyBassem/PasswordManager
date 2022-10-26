using Manager.BL.DTOs;
using Manager.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Manager.BL.Manager
{
    public interface IManager
    {
        IEnumerable<UserReadDto> GetAll();
        Task<UserReadDto> GetById(string id);
        Task<Employee> GetCurrentUser(ClaimsPrincipal User);
        Task<UserReadDto> Add(UserRegisterDto student);
        Task<bool> Update(UserUpdateDto student);
        Task<string> CheckLogin(UserLoginDto student);
        Task<bool> DeleteById(string id);
    }
}
