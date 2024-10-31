using LoveLetters.Repository.Context;

namespace LoveLetters.Repository.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<users> getUserByEmail(string email);
    }
}
