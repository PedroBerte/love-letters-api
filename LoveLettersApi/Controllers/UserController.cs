using LoveLetters.Service.DTO.Responses;
using LoveLetters.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoveLetters.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult> GetUser(string? name, string? email, bool? havePartner)
        {
            try
            {
                return Ok(await userService.GetUsers(name, email, havePartner));
            }
            catch (Exception ex)
            {
                return BadRequest(new DefaultResponse<object>()
                {
                    Code = 500,
                    Message = "Erro desconhecido no serviço de busca de usuário.",
                    Data = ex,
                    Success = false
                });
            }
        }

        [HttpGet]
        [Route("Get/{guid}")]
        public async Task<ActionResult> GetUser(string guid)
        {
            return Ok();
        }
    }
}
