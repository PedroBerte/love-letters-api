using LoveLetters.Repository.Context;
using LoveLetters.Repository.Repositories.Interfaces;
using LoveLetters.Service.DTO.Responses;
using LoveLetters.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Service.Services
{
    public class RelationshipService : IRelationshipService
    {
        private IRelationshipRepository relationshipRepository;
        public RelationshipService(IRelationshipRepository relationshipRepository) 
        { 
            this.relationshipRepository = relationshipRepository;
        }

        public async Task<DefaultResponse<Invites>> InvitePartner(string guidInviter, string guidInvited)
        {
            try
            {
                var alreadyInvited = await relationshipRepository.GetInvite(guidInviter, guidInvited);

                if (alreadyInvited != null)
                {
                    return new DefaultResponse<Invites>()
                    {
                        Code = 403,
                        Message = $"Esse convite já existe.",
                        Success = false,
                    };
                }

                var invite = await relationshipRepository.InsertInvite(guidInviter, guidInvited);

                return new DefaultResponse<Invites>()
                {
                    Code = 200,
                    Data = invite,
                    Success = true,
                    Message = "Invite enviado com sucesso!"
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
        }
    }
}
