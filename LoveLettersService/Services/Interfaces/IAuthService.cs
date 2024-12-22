using LoveLetters.Repository.Context;
using LoveLetters.Service.DTO;
using LoveLetters.Service.DTO.Responses;

namespace LoveLetters.Service.Services.Interfaces
{
    public interface IAuthService
    {
        Task<DefaultResponse<UsersDTO>> LoginUser(string email, string password);
        Task<DefaultResponse<UsersDTO>> RegisterUser(UsersDTO user);
    }
}
