using LoveLetters.Service.DTO.Responses;
using LoveLetters.Service.Services;
using LoveLetters.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoveLetters.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RelationshipController : ControllerBase
    {
        private readonly IRelationshipService relationshipService;
        public RelationshipController(IRelationshipService relationshipService) 
        {
            this.relationshipService = relationshipService;
        }

        [HttpGet]
        [Route("invites")]
        public async Task<ActionResult> GetInvitesByUser(string userGuid)
        {
            try
            {
                return Ok(await relationshipService.GetInvitesByUser(userGuid));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DefaultResponse<object>
                {
                    Code = 500,
                    Message = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.",
                    Data = null,
                    Success = false
                });
            }
        }

        [HttpPost]
        [Route("invitePartner")]
        public async Task<ActionResult> InvitePartner(string inviterGuid, string invitedGuid)
        {
            try
            {
                return Ok(await relationshipService.InvitePartner(inviterGuid, invitedGuid));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DefaultResponse<object>
                {
                    Code = 500,
                    Message = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.",
                    Data = null, 
                    Success = false
                });
            }
        }

        [HttpPost]
        [Route("acceptInvite")]
        public async Task<ActionResult> AcceptInvite(int inviteId)
        {
            try
            {
                return Ok(await relationshipService.AcceptInvite(inviteId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DefaultResponse<object>
                {
                    Code = 500,
                    Message = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.",
                    Data = null,
                    Success = false
                });
            }
        }
    }
}
