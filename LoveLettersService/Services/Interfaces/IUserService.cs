using LoveLetters.Service.DTO;
using LoveLetters.Service.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<DefaultResponse<List<UsersDTO>>> GetUsers(string? name, string? email, bool? havePartner);
    }
}
