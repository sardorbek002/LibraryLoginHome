using Libraray.Application.Services;
using Library.Domain.Entities;
using Library.Domain.Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Library.Infrastructure.Services
{
    public class TokenService : ITokenSrvice
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Token> GenerateTokenAsync(User user)
        {
        

        List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim("Id",user.Id.ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            double AccessTokenLifeTime = (double.Parse(_configuration["JWT:AccesTokenLifetimeINMinutes"]));
            var token = new JwtSecurityToken(
                expires:DateTime.Now.AddMinutes(AccessTokenLifeTime),
                signingCredentials:credentials,
                claims:claims);    
            string accessToken= new JwtSecurityTokenHandler().WriteToken(token);
            return new()
            {
                AccesToken=accessToken,
                RefreshToken=await RefreshTokenAsync()
            };
        }

        public async Task<User> GetClaimFromExpiredTokenAsync(string accesToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accesToken);
            var claims = (jsonToken as JwtSecurityToken)?.Claims;
            var userClaims = claims?.ToArray();
            return new User()
            {
                Id = int.Parse(userClaims.First(x => x.Type.Equals("Id")).Value),
                Name = userClaims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value
            };
           
            

        }

        public Task<Token> GetNewTokenFromExpiredTokenAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<string> RefreshTokenAsync()
        {
            string token = ComputeSHA256Hash(DateTime.Now.ToString() + "MayKey");
            return Task.FromResult(token);
        }


        public string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes =Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                // Convert the byte array to a hexadecimal string
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
