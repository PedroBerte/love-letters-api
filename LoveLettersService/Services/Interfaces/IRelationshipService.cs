using LoveLetters.Repository.Context;
using LoveLetters.Service.DTO.Responses;

namespace LoveLetters.Service.Services.Interfaces
{
    public interface IRelationshipService
    {
        Task<DefaultResponse<Invites>> InvitePartner(string guidInviter, string guidInvited);
    }
}
