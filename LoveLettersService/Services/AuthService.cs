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

        public async Task<DefaultResponse<string>> LoginUser(string email, string password)
        {
            try
            {
                var user = await authRepository.GetUserByEmail(email);

                if (user == null)
                {
                    return new DefaultResponse<string>()
                    {
                        Code = 403,
                        Message = "Email e/ou senha incorreta!",
                        Success = false,
                    };
                }

                string passwordHashed = new PasswordHelper(configuration["Salt"]).HashPassword(password);

                if (passwordHashed == user.password)
                {
                    return new DefaultResponse<string>()
                    {
                        Code = 200,
                        Data = new JwtTokenService(configuration["SecretKey"]).GenerateUserToken(user),
                        Success = true,
                        Message = "Login efetuado com sucesso!"
                    };
                }
                else
                {
                    return new DefaultResponse<string>()
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

        public async Task<DefaultResponse<string>> RegisterUser(Users user)
        {
            try
            {
                var hasUser = await authRepository.GetUserByEmail(user.email);

                if (hasUser != null)
                {
                    return new DefaultResponse<string>
                    {
                        Code = 403,
                        Message = "E-Mail já cadastrado no sistema.",
                        Success = false
                    };
                }

                var guid = Guid.NewGuid();
                user.guid = guid.ToString();

                var hashedPassword = new PasswordHelper(configuration["Salt"]).HashPassword(user.password);
                user.password = hashedPassword;

                await authRepository.InsertUser(user);

                return new DefaultResponse<string>()
                {
                    Code = 200,
                    Data = guid.ToString(),
                    Success = true,
                    Message = "Login efetuado com sucesso!"
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }
    }
}
