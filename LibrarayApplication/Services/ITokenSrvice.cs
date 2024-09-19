using Library.Domain.Entities;
using Library.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraray.Application.Services
{
    public  interface ITokenSrvice
    {
        public Task <Token> GenerateTokenAsync(User user);
        public Task <string> RefreshTokenAsync();
        public Task <User> GetClaimFromExpiredTokenAsync( string accesToken);
        public Task <Token> GetNewTokenFromExpiredTokenAsync(User user);
        string ComputeSHA256Hash(string input);
    }
}
