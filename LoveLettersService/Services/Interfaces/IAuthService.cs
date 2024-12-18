using LoveLetters.Repository.Context;
using LoveLetters.Service.Responses;

namespace LoveLetters.Service.Services.Interfaces
{
    public interface IAuthService
    {
        Task<DefaultResponse<Users>> LoginUser(string email, string password);
        Task<DefaultResponse<Users>> RegisterUser(Users user);
    }
}
