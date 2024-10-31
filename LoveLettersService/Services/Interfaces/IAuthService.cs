using LoveLetters.Service.Responses;

namespace LoveLetters.Service.Services.Interfaces
{
    public interface IAuthService
    {
        Task<DefaultResponse<string>> LoginUser(string email, string password);
    }
}
