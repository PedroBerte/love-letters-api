using LoveLetters.Repository.Context;
using LoveLetters.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Repository.Repositories
{
    public class RelationshipRepository : IRelationshipRepository
    {
        private LoveLettersContext _context;
        public RelationshipRepository(LoveLettersContext context) 
        {
            _context = context;
        }

        public async Task<List<Invites>> GetInvite(string guidInviter, string guidInvited)
        {
            try
            {
                var invite = await _context.Invites.Where(x => x.guidInviter == guidInviter && x.guidInvited == guidInvited).ToListAsync();

                return invite;
            }
            catch (DbException)
            {
                throw;
            }
        }

        public async Task<Invites> InsertInvite(string guidInviter, string guidInvited)
        {
            try
            {
                var newInvite = new Invites()
                {
                    guidInviter = guidInviter,
                    guidInvited = guidInvited,
                    inviteAccepted = false,
                    inviteRejected = false,
                    inviteDate = DateTime.Now
                };

                var invite = _context.Invites.Add(newInvite);
                await _context.SaveChangesAsync();

                return invite.Entity;
            }
            catch (DbException)
            {
                throw;
            }
        }
    }
}
