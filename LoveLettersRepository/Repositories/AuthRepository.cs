using LoveLetters.Repository.Context;
using LoveLetters.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace LoveLetters.Repository.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly LoveLettersContext context;
        public AuthRepository(LoveLettersContext context)
        {
            this.context = context;
        }

        public async Task<Users> GetUserByEmail(string email)
        {
            try
            {
                return await context.Users.FirstOrDefaultAsync(x => x.email == email);
            }
            catch (DbException)
            {
                throw;
            }
        }

        public async Task InsertUser(Users user)
        {
            try
            {
                await context.Users.AddAsync(user);
            }
            catch (DbException)
            {
                throw;
            }
        }
    }
}
