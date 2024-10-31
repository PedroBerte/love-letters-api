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

        public async Task<users> getUserByEmail(string email)
        {
            try
            {
                return await context.users.FirstOrDefaultAsync(x => x.email == email);
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }
    }
}
