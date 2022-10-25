using AutoMapper;
using Manager.BL.DTOs;
using Manager.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Manager.BL.Manager
{
    public class Manager : IManager
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IConfiguration _configuration;
        private IMapper _mapper;
        public Manager(
            UserManager<Employee> userManager,
            IConfiguration configuration,
             IMapper mapper)
        {

            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;

        }
        public async Task<UserReadDto> Add(UserRegisterDto emp)
        {
            var newEmp = _mapper.Map<Employee>(emp);
            var createdEmp = await _userManager.CreateAsync(newEmp,emp.Password);
            if (!createdEmp.Succeeded)
            {
                UserReadDto userReadDto = new UserReadDto { ErrorMessage = createdEmp.Errors };
                return userReadDto;
            }
                
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,newEmp.Id),
                new Claim(ClaimTypes.Role,emp.Role)

            };
            var claimResult = await _userManager.AddClaimsAsync(newEmp, claims);
            return _mapper.Map<UserReadDto>(newEmp);
        }

        public void DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public  IEnumerable<UserReadDto> GetAll()
        {
            var user =  _userManager.Users.ToList();

            return _mapper.Map<IEnumerable<UserReadDto>>(user);
        }

        public async Task<UserReadDto>? GetById(int id)
        {
           
            var user = await _userManager.FindByIdAsync(id.ToString());
            return _mapper.Map<UserReadDto>(user);
        }

        public bool Update(UserRegisterDto student)
        {
            throw new NotImplementedException();
        }
    }
}
