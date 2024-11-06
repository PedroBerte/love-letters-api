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
        public async Task<ActionResult> Login(string email, string password)
        {
            try
            {
                return Ok(await authService.LoginUser(email, password));
            }
            catch (Exception)
            {
                return BadRequest(new DefaultResponse<object>()
                {
                    Code = 500,
                    Message = "Erro desconhecido no serviço de Login.",
                    Success = true
                });
            }
        }

    }
}
