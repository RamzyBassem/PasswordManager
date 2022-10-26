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
            return Ok(check);
        }
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(UserRegisterDto register)
        {
            var user = await manager.Add(register);
            if (user.ErrorMessage.Count() > 0)
            {
                return BadRequest(user.ErrorMessage);
            }
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(manager.GetAll());
        }
    }
}
