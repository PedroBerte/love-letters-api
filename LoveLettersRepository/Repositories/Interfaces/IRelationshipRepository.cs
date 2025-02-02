using LoveLetters.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Repository.Repositories.Interfaces
{
    public interface IRelationshipRepository
    {
        Task<List<Invites>> GetInvite(string guidInviter, string guidInvited);
        Task<Invites> InsertInvite(string guidInviter, string guidInvited);
        Task<Invites> AcceptInvite(int inviteId);
        Task<List<Invites>> GetInvites(string userGuid);
    }
}
