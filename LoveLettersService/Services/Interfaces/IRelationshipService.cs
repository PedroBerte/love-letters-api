using LoveLetters.Repository.Context;
using LoveLetters.Service.DTO;
using LoveLetters.Service.DTO.Responses;

namespace LoveLetters.Service.Services.Interfaces
{
    public interface IRelationshipService
    {
        Task<DefaultResponse<InvitesDTO>> InvitePartner(string guidInviter, string guidInvited);
        Task<DefaultResponse<InvitesDTO>> AcceptInvite(int inviteId);
        Task<DefaultResponse<List<InvitesDTO>>> GetInvitesByUser(string userGuid);
    }
}
