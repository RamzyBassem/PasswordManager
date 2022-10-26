using AutoMapper;
using Manager.BL.DTOs;
using Manager.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.BL.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, UserReadDto>();
            CreateMap<UserRegisterDto, Employee>();
            CreateMap<UserLoginDto, Employee>();
            CreateMap<UserUpdateDto, Employee>().ForSourceMember(x => x.Id, y => y.DoNotValidate());

        }
    }
}
