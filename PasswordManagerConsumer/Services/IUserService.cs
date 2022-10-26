using PasswordManagerConsumer.Models;

namespace PasswordManagerConsumer.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Employee>> GetUsers();
    }
}
