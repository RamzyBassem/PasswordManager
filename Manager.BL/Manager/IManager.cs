using Manager.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.BL.Manager
{
    public interface IManager
    {
        IEnumerable<UserReadDto> GetAll();
        Task<UserReadDto> GetById(int id);
        Task<UserReadDto> Add(UserRegisterDto student);
        bool Update(UserRegisterDto student);
        void DeleteById(Guid id);
    }
}
