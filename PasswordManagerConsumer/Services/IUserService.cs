using PasswordManagerConsumer.Dtos;
using PasswordManagerConsumer.Models;

namespace PasswordManagerConsumer.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Employee>> GetUsers();
        Task<string> CheckLogin(UserLoginDto user);
        Task<string> Register(UserRegisterDto user);
    }
}
