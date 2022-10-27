using Manager.BL.DTOs;
using Manager.BL.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace PasswordManagerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private IManager manager;
        public ManagerController(IManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(UserLoginDto login)
        {
            var check = await manager.CheckLogin(login);
            if (check == "UnAuthorized")
            {
                return Unauthorized();
            }
            return Ok(new { token = check });
        }
        [HttpPost]
        [Route("Register")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Register(UserRegisterDto register)
        {
            var user = await manager.Add(register);
            if (user.ErrorMessage.Count() > 0)
            {
                return BadRequest(new
                {
                    ErrorMessage = user.ErrorMessage,
                    UserNameExists = user.UserNameExists
                });
            }
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(manager.GetAll());
        }
        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> GetById(string id)
        {
        
          
            var user = await manager.GetById(id);
            if (user == null)
                return BadRequest();
            return Ok(user);
        }
        [HttpPut("Edit/{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Update(UserUpdateDto user,string id)
        {
            Console.WriteLine(user.UserName);
            if(user.Id!=id)
            {
                return BadRequest();
            }           
            var userUpdated = await manager.Update(user);
            if (!userUpdated)
                return NotFound();
            else
                return Ok();
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Delete( string id)
        {
       
            var userUpdated = await manager.DeleteById(id);
            if (!userUpdated)
                return NotFound();
            else
                return Ok();
        }
    }
}
