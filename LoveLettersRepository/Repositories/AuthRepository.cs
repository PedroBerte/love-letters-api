using LoveLetters.Repository.Context;
using LoveLetters.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return await context.users.FirstOrDefaultAsync(x => x.email == email);   
        }
    }
}
