using BasicLibrary.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace ManagementNetBlazorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController (IUserAccount accountInterface): ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult> CreateAsync(Register user)
        {
            if (user == null) return BadRequest("Model is empty!");
            var result = await accountInterface.CreateAsync(user);
            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<ActionResult> SignInAsync(Login user)
        {
            if (user == null) return BadRequest("Model is empty!");
            var result = await accountInterface.SignInAsync(user);
            return Ok(result);
        }


        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshTokenAsync(RefreshToken token)
        {
            if (token == null) return BadRequest("Model is empty!");
            var result = await accountInterface.RefreshTokenAsync(token);
            return Ok(result);
        }
    }
}
