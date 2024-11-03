using LoveLetters.Repository.Context;
using LoveLetters.Service.Responses;
using LoveLetters.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoveLetters.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> LoginUser(string email, string password)
        {
            try
            {
                return Ok(await authService.LoginUser(email, password));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> RegisterUser([FromBody] users user)
        {
            return Ok();
        }
    }
}
