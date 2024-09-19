using Libraray.Application.Services;
using Library.Domain.Entities;
using Library.Domain.Entities.Models;
using Library.Domain.Models;
using Library.Infrastructure.Persistance;
using Library.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace Libraray.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        // private readonly IIdentityService _identityService;
        private readonly ITokenSrvice _tokenSrvice;
        private readonly IdentityDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public IdentityService(IdentityDbContext dbContext, ITokenSrvice tokenSrvice, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _tokenSrvice = tokenSrvice;
            _configuration = configuration;
        }

        public Task<Response<bool>> DeleteUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsValidRefreshToken(string refreshtoken, int userId)
        {
            RefreshToken? refreshTokenentity;
            var result = _dbContext.RefreshTokens.Where(x =>
             x.UserID.Equals(user.Id) && x.RefreshTokenValue.Equals(refreshtoken));
            if (result.Count() != 1)
            {
                return false;
            }
            refreshTokenentity = result.First();
            if (refreshTokenentity.ExpiretTime < DateTime.Now)
            {
                return false;
            }
            return true;
        }

        public async Task<Response<(User, Token)>> Login(Credential credential)
        {
            credential.Password = _tokenSrvice.ComputeSHA256Hash(credential.Password);

            User? user = _dbContext.Users.FirstOrDefault(x =>
                x.UserName.Equals(credential.UserName) &&
                x.Password.Equals(credential.Password)
            );

            if (user == null)
            {
                return new Response<(User, Token)>("Нотўғри аутентификация маълумотлари", 401);
              
            }

            Token userToken = await _tokenSrvice.GenerateTokenAsync(user);
            bool isSuccess = await SaveRefreshToken(userToken.RefreshToken, user);

            return isSuccess
                ? new Response<(User, Token)>((user, userToken))
                : new Response<(User, Token)>("Refresh токенини сақлашда хатолик юз берди!!!");
        }



        public async Task<Response<bool>> Logout()
        {


            //  _dbContext.Remove(RefreshTokens);
            //var res= await _dbContext.SaveChangesAsync();

            //if (res>0)
            //{
            //    return new Response<bool>(true);
            //}
            //else
            //{
            //    return new Response<bool>(false);
            //}
            return new(true);
        }

        public async Task<Response<(User, Token)>> RefreshTokenAsync(Token token)
        {
            User user = await _tokenSrvice.GetClaimFromExpiredTokenAsync(token.AccesToken);

            if (!await IsValidRefreshToken(token.RefreshToken, user.Id))
            {
                return new Response<(User, Token)>("RefreshToken Invalid!!!!");
            }

            Token newToken = await _tokenSrvice.GenerateTokenAsync(user);
            bool isSuccess = await SaveRefreshToken(newToken.RefreshToken, user);

            return isSuccess ? new Response<(User, Token)>((user, newToken)) : new Response<(User, Token)>("Failed");
        }


        public async Task<Response<(User, Token)>> Register(User user)
        {
            user.Password=_tokenSrvice.ComputeSHA256Hash(user.Password);
            await _dbContext.Users.AddAsync(user);
            int effectedRows = await _dbContext.SaveChangesAsync();

            if (effectedRows <= 0)
                return new Response<(User, Token)>("Operation Failed!");

            Token token = await _tokenSrvice.GenerateTokenAsync(user);
            bool isSuccess = await SaveRefreshToken(token.RefreshToken, user);

            return isSuccess ? new((user, token)) : new("Failed!");
        }
        User user = new();
        public async Task<bool> SaveRefreshToken(string refreshToken, User user)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return false;
            }
            RefreshToken? refreshTokenEntity;
            var result = _dbContext.RefreshTokens.Where(x =>
            x.UserID.Equals(user.Id) && x.RefreshTokenValue.Equals(refreshToken));
            if (result.Count() == 0)
            {
                refreshTokenEntity = new()
                {
                    ExpiretTime = DateTime.Now.AddMinutes(7),
                    RefreshTokenValue = refreshToken,
                    UserID = user.Id
                };
                _dbContext.RefreshTokens.AddAsync(refreshTokenEntity);
            }

            else if (result.Count() == 1)
            {
                refreshTokenEntity = result.First();
                refreshTokenEntity.RefreshTokenValue = refreshToken;
                refreshTokenEntity.ExpiretTime = DateTime.Now.AddMinutes(7);
                _dbContext.RefreshTokens.Update(refreshTokenEntity);
            }
            else
                return false;




            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }


    }
}
