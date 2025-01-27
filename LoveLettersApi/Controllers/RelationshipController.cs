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

        [HttpPost]
        [Route("invitePartner")]
        public async Task<ActionResult> InvitePartner(string guidInviter, string guidInvited)
        {
            try
            {
                return Ok(await relationshipService.InvitePartner(guidInviter, guidInvited));
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

        //[HttpPost]
        //[Route("acceptInvite")]
        //public async Task<ActionResult> AcceptInvite(int inviteId)
        //{
        //    try
        //    {
        //        return Ok(await relationshipService.InvitePartner(inviteId));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new DefaultResponse<object>
        //        {
        //            Code = 500,
        //            Message = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.",
        //            Data = null,
        //            Success = false
        //        });
        //    }
        //}
    }
}
