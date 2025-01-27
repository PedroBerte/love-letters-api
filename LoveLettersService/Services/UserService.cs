using AutoMapper;
using LoveLetters.Repository.Context;
using LoveLetters.Repository.Repositories.Interfaces;
using LoveLetters.Service.DTO;
using LoveLetters.Service.DTO.Responses;
using LoveLetters.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<DefaultResponse<List<UsersDTO>>> GetUsers(string? name, string? email, bool? havePartner)
        {
            var usersEntities = await userRepository.GetUsers(name, email, havePartner);
            var users = mapper.Map<List<UsersDTO>>(usersEntities);

            return new DefaultResponse<List<UsersDTO>>()
            {
                Code = 200,
                Data = users,
                Success = true,
            };
        }
    }
}
