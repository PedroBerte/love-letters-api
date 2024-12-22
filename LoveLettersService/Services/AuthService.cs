using AutoMapper;
using LoveLetters.Repository.Context;
using LoveLetters.Repository.Repositories.Interfaces;
using LoveLetters.Service.DTO;
using LoveLetters.Service.DTO.Responses;
using LoveLetters.Service.Helpers;
using LoveLetters.Service.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LoveLetters.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        public AuthService(IAuthRepository authRepository, IConfiguration configuration, IMapper mapper)
        {
            this.authRepository = authRepository;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<DefaultResponse<UsersDTO>> LoginUser(string email, string password)
        {
            try
            {
                var user = mapper.Map<UsersDTO>(await authRepository.GetUserByEmail(email));

                if (user is null)
                {
                    return new DefaultResponse<UsersDTO>()
                    {
                        Code = 403,
                        Message = "Email e/ou senha incorreta!",
                        Success = false,
                    };
                }

                string passwordHashed = new PasswordHelper(configuration["Salt"]).HashPassword(password);

                if (passwordHashed == user.password)
                {
                    var jwtToken = new JwtTokenService(configuration["SecretKey"]).GenerateUserToken(user);
                    user.jwtToken = jwtToken;

                    return new DefaultResponse<UsersDTO>()
                    {
                        Code = 200,
                        Data = user,
                        Success = true,
                        Message = "Login efetuado com sucesso!"
                    };
                }
                else
                {
                    return new DefaultResponse<UsersDTO>()
                    {
                        Code = 403,
                        Message = "Email e/ou senha incorreta!",
                        Success = false,
                    };
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<DefaultResponse<UsersDTO>> RegisterUser(UsersDTO user)
        {
            try
            {
                var possibleUser = mapper.Map<UsersDTO>(await authRepository.GetUserByEmail(user.email));

                if (possibleUser != null)
                {
                    return new DefaultResponse<UsersDTO>
                    {
                        Code = 403,
                        Message = "E-Mail já cadastrado no sistema.",
                        Success = false
                    };
                }

                var guid = Guid.NewGuid();
                var hashedPassword = new PasswordHelper(configuration["Salt"]).HashPassword(user.password);
                var jwtToken = new JwtTokenService(configuration["SecretKey"]).GenerateUserToken(user);

                user.guid = guid.ToString();
                user.password = hashedPassword;
                user.jwtToken = jwtToken;

                await authRepository.InsertUser(mapper.Map<Users>(user));

                return new DefaultResponse<UsersDTO>()
                {
                    Code = 200,
                    Data = user,
                    Success = true,
                    Message = "Registro efetuado com sucesso!"
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }
    }
}
