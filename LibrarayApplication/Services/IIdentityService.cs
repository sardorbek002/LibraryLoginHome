using Library.Domain.Entities;
using Library.Domain.Entities.Models;
using Library.Domain.Models;

namespace Libraray.Application.Services
{
    public interface IIdentityService
    {
        public Task<Response<(User, Token)>> Register(User user);
        public Task<Response<(User, Token)>> Login(Credential credential);
        public Task<Response<bool>> Logout();
        public Task<Response<(User, Token)>> RefreshTokenAsync(Token token);
        public Task<Response<bool>> DeleteUserAsync(int userId);
        public Task<bool> SaveRefreshToken(string refreshtoken,User user);
        public Task<bool> IsValidRefreshToken(string refreshtoken, int userId);


    }
}
