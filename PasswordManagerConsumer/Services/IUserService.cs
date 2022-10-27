using PasswordManagerConsumer.Dtos;
using PasswordManagerConsumer.Models;

namespace PasswordManagerConsumer.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Employee>> GetUsers();
        Task<string> CheckLogin(UserLoginDto user);
        Task<string> Register(UserRegisterDto user);
        Task<Employee> GetUserById(string id);
        Task<string> EditUser(Employee emp);
    }
}
