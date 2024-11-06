using LoveLetters.Repository.Context;

namespace LoveLetters.Repository.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<Users> GetUserByEmail(string email);
        Task InsertUser(Users user);
    }
}
