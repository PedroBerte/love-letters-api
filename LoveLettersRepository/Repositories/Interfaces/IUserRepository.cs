using LoveLetters.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Repository.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<Users>> GetUsers(string? name, string? email, bool? havePartner);
    }
}
