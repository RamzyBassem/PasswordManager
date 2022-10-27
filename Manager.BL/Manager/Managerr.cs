using AutoMapper;
using Manager.BL.DTOs;
using Manager.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Slapper.AutoMapper;

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
        // Register New User
        public async Task<UserReadDto> Add(UserRegisterDto emp)
        {
            var newEmp = _mapper.Map<Employee>(emp);
            var user = await _userManager.FindByNameAsync(newEmp.UserName);

            
                var createdEmp = await _userManager.CreateAsync(newEmp, emp.Password);
                if (!createdEmp.Succeeded)
                {
                    UserReadDto userReadDto = new UserReadDto { ErrorMessage = createdEmp.Errors };
                    if (user != null)
                    {
                        userReadDto.UserNameExists = true;
                    }
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
        // Check If User Exists
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
        // Delete  a user By ID
        public async Task<bool> DeleteById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return false;
          await _userManager.DeleteAsync(user);
            return true;

        }
        // Get ALl Users
        public  IEnumerable<UserReadDto> GetAll()
        {
            var users =  _userManager.Users.ToList();
            List<UserReadDto> userReadDtos= new List<UserReadDto>();
            var dto = new UserReadDto();
            foreach (var user in users)
            {
                dto= _mapper.Map<UserReadDto>(user);
                dto.Role = _userManager.GetClaimsAsync(user).Result[1].Value;
                userReadDtos.Add(dto);
            }

            return userReadDtos;
        }
        // Get user By Id
        public async Task<UserReadDto>? GetById(string id)
        {
           
            var user = await _userManager.FindByIdAsync(id);
            
            return _mapper.Map<UserReadDto>(user);
        }
        // Get Current Logged In User
        public async Task<Employee> GetCurrentUser(ClaimsPrincipal User)
        {
            var user = await _userManager.GetUserAsync(User);
            return (user);
        }
        // Update User
        public async Task<bool> Update(UserUpdateDto student)
        {
            var user = await _userManager.FindByIdAsync(student.Id);
            if (user == null)
                return false;
            _mapper.Map<UserUpdateDto, Employee>(student, user);
            var claimNew = new Claim(ClaimTypes.Role, student.Role);
            var claimOld = _userManager.GetClaimsAsync(user).Result[1];
             await _userManager.ReplaceClaimAsync(user, claimOld, claimNew);
             await _userManager.UpdateAsync(user);
            
            return true;
        }
    }
}
