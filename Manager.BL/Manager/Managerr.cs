using AutoMapper;
using Manager.BL.DTOs;
using Manager.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Manager.BL.Manager
{
    public class Managerr : IManager
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IConfiguration _configuration;
        private IMapper _mapper;
        public Managerr(
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
            var User = _mapper.Map<UserReadDto>(newEmp);
            User.Role = emp.Role;
            return User;
        }

        public async Task<string> CheckLogin(UserLoginDto empLogin)
        {
            var user = await _userManager.FindByNameAsync(empLogin.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, empLogin.Password))
            {
                return "UnAuthorized";
            }
            var claims = await _userManager.GetClaimsAsync(user);
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(

                expires: DateTime.Now.AddHours(3),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
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
