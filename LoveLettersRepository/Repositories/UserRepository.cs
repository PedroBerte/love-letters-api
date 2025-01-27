using LoveLetters.Repository.Context;
using LoveLetters.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;


namespace LoveLetters.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LoveLettersContext context;
        public UserRepository(LoveLettersContext context)
        {
            this.context = context;    
        }

        public async Task<List<Users>> GetUsers(string? name, string? email, bool? havePartner)
        {
            try
            {
                var query = context.Users.AsQueryable();

                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(u => u.name.Contains(name));

                if (!string.IsNullOrWhiteSpace(email))
                    query = query.Where(u => u.email.Contains(email));

                if (havePartner.HasValue)
                    query = query.Where(u => u.havePartner == havePartner.Value);

                var users = await query.ToListAsync();
                users.ForEach(x => x.password = null);
                return users;
            }
            catch (DbException)
            {

                throw;
            }

        }
    }
}
