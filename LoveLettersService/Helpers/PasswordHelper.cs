using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoveLetters.Service.Helpers
{
    public class PasswordHelper
    {
        private readonly string secretSalt;
        public PasswordHelper(string secretSalt)
        {
            this.secretSalt = secretSalt;
        }
        private string GenerateSalt()
        {
            try
            {
                var saltBytes = Encoding.UTF8.GetBytes(secretSalt);
                return Convert.ToBase64String(saltBytes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string HashPassword(string password)
        {
            try
            {
                using (var sha256 = SHA256.Create())
                {
                    var combinedPassword = $"{GenerateSalt()}{password}";
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(combinedPassword);
                    byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                    return Convert.ToBase64String(hashBytes);
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao gerar o hash da senha: {ex}");
            }
        }
    }
}
