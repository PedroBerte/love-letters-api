using LoveLetters.Repository.Context;
using LoveLetters.Repository.Repositories.Interfaces;
using LoveLetters.Service.Helpers;
using LoveLetters.Service.Responses;
using LoveLetters.Service.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LoveLetters.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository authRepository;
        private readonly IConfiguration configuration;
        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            this.authRepository = authRepository;
            this.configuration = configuration;
        }

        public async Task<DefaultResponse<Users>> LoginUser(string email, string password)
        {
            try
            {
                var user = await authRepository.GetUserByEmail(email);

                if (user is null)
                {
                    return new DefaultResponse<Users>()
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

                    return new DefaultResponse<Users>()
                    {
                        Code = 200,
                        Data = user,
                        Success = true,
                        Message = "Login efetuado com sucesso!"
                    };
                }
                else
                {
                    return new DefaultResponse<Users>()
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

        public async Task<DefaultResponse<Users>> RegisterUser(Users user)
        {
            try
            {
                var hasUser = await authRepository.GetUserByEmail(user.email);

                if (hasUser != null)
                {
                    return new DefaultResponse<Users>
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

                await authRepository.InsertUser(user);

                return new DefaultResponse<Users>()
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
