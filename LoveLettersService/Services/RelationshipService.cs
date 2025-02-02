using AutoMapper;
using LoveLetters.Repository.Context;
using LoveLetters.Repository.Repositories.Interfaces;
using LoveLetters.Service.DTO;
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
        private IDomainService domainService;
        private IMapper mapper;
        public RelationshipService(IRelationshipRepository relationshipRepository, IDomainService domainService, IMapper mapper) 
        { 
            this.relationshipRepository = relationshipRepository;
            this.domainService = domainService;
            this.mapper = mapper;
        }

        public async Task<DefaultResponse<List<InvitesDTO>>> GetInvitesByUser(string userGuid)
        {
            try
            {
                var inviteEntities = await relationshipRepository.GetInvites(userGuid);
                var invites = mapper.Map<List<InvitesDTO>>(inviteEntities);

                foreach(var invite in invites)
                    invite.status_domain = await domainService.GetSubDomain("invite_status", invite.inviteStatus);

                return new DefaultResponse<List<InvitesDTO>>()
                {
                    Code = 200,
                    Data = invites,
                    Success = true,
                    Message = "Invite enviado com sucesso!"
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<DefaultResponse<InvitesDTO>> InvitePartner(string guidInviter, string guidInvited)
        {
            try
            {
                var alreadyInvited = await relationshipRepository.GetInvite(guidInviter, guidInvited);

                if (alreadyInvited != null)
                {
                    return new DefaultResponse<InvitesDTO>()
                    {
                        Code = 403,
                        Message = $"Esse convite já existe.",
                        Success = false,
                    };
                }

                var inviteEntities = await relationshipRepository.InsertInvite(guidInviter, guidInvited);
                var invite = mapper.Map<InvitesDTO>(inviteEntities);
                invite.status_domain = await domainService.GetSubDomain("invite_status", invite.inviteStatus);

                return new DefaultResponse<InvitesDTO>()
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

        public async Task<DefaultResponse<InvitesDTO>> AcceptInvite(int inviteId)
        {
            try
            {
                var inviteEntities = await relationshipRepository.AcceptInvite(inviteId);
                var invite = mapper.Map<InvitesDTO>(inviteEntities);

                var domain = await domainService.GetSubDomain("invite_status", inviteEntities.inviteStatus);
                
                invite.status_domain = domain;

                return new DefaultResponse<InvitesDTO>
                {
                    Code = 200,
                    Data = invite,
                    Success = true,
                    Message = "Convite aceito."
                };
            }
            catch (ApplicationException)
            {
                throw;
            }
        } 
    }
}
