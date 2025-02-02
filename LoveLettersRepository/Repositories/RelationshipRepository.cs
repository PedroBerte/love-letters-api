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

        public async Task<List<Invites>> GetInvites(string userGuid)
        {
            try
            {
                return await _context.Invites.Where(x => x.guidInviter == userGuid || x.guidInvited == userGuid).ToListAsync();
            }
            catch (DbException)
            {
                throw;
            }
        }

        public async Task<Invites> GetInvite(int id)
        {
            try
            {
                return await _context.Invites.FirstOrDefaultAsync(x => x.id == id);
            }
            catch (DbException)
            {
                throw;
            }
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
                    inviteStatus = "SD", //domain ->  send
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

        public async Task<Invites> AcceptInvite(int inviteId)
        {
            try
            {
                var invite = await _context.Invites.FirstOrDefaultAsync(x => x.id == inviteId);

                if (invite is null)
                    throw new Exception("Convite não encontrado.");

                invite.inviteStatus = "AC";
                invite.inviteUpdateDate = DateTime.Now;

                _context.Invites.Add(invite);
                await _context.SaveChangesAsync();

                return invite;
            }
            catch (DbException)
            {
                throw;
            }
        }
    }
}
