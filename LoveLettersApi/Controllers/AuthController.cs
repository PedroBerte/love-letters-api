using LoveLetters.Repository.Context;
using LoveLetters.Service.DTO;
using LoveLetters.Service.DTO.Responses;
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
        [Route("Login/{email}/{password}")]
        public async Task<ActionResult> LoginUser(string email, string password)
        {
            try
            {
                return Ok(await authService.LoginUser(email, password));
            }
            catch (Exception ex)
            {
                return BadRequest(new DefaultResponse<object>()
                {
                    Code = 500,
                    Message = "Erro desconhecido no serviço de Login.",
                    Data = ex,
                    Success = false
                });
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> RegisterUser([FromBody] UsersDTO user)
        {
            try
            {
                return Ok(await authService.RegisterUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(new DefaultResponse<object>()
                {
                    Code = 500,
                    Message = "Erro desconhecido no serviço de Registro.",
                    Data = ex,
                    Success = false
                });
            }
        }
    }
}
