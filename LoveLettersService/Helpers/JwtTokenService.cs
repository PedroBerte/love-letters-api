using LoveLetters.Repository.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoveLetters.Service.Helpers
{
    public class JwtTokenService
    {
        private readonly string secret;
        public JwtTokenService(string secret)
        {
            this.secret = secret;
        }

        public string GenerateUserToken(users user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: getUserClaims(user),
                expires: DateTime.UtcNow.AddMonths(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Claim[] getUserClaims(users user)
        {
            var claims = new Claim[]
            {
                new Claim("uid", user.uid),
                new Claim("name", user.name),
                new Claim("email", user.email),
                new Claim("profilePhoto", user?.profilePhoto ?? ""),
                new Claim("partnerUid", user ?.partnerUID ?? ""),
                new Claim("partnerName", user?.partnerName ?? ""),
                new Claim("havePartner", user.havePartner ? "1" : "0")
            };
            return claims;
        }
    }
}
