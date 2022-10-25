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
        Task<IEnumerable<UserReadDto>> GetAll();
        Task<UserReadDto>? GetById(Guid id);
        Task<UserReadDto> Add(StudentAddDTO student);
        bool Update(StudentUpdateDto student);
        void DeleteById(Guid id);
    }
}
