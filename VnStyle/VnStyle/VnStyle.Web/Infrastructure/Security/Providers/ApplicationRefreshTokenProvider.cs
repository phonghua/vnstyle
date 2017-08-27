using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;
using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.ObjectContainer;
using VnStyle.Services.Business;
using VnStyle.Services.Data.Domain.Memberships;

namespace VnStyle.Web.Infrastructure.Security.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationRefreshTokenProvider : IAuthenticationTokenProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Create(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");
            var userService = EngineContext.Current.Resolve<IUserService>();
            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var token = new AspNetRefreshToken()
            {
                TokenId = CommonHelper.GetHashId(refreshTokenId),
                ClientId = clientid,
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            var result = userService.AddRefreshToken(token);

            if (result)
            {
                context.SetToken(refreshTokenId);
            }
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            Create(context);
            return Task.FromResult<object>(null);
        }


        public void Receive(AuthenticationTokenReceiveContext context)
        {

        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = CommonHelper.GetHashId(context.Token);

           // using (AuthRepository _repo = new AuthRepository())
            {
                var userService = EngineContext.Current.Resolve<IUserService>();
                var refreshToken = userService.FindRefreshToken(hashedTokenId);

                if (refreshToken != null)
                {
                    //Get protectedTicket from refreshToken class
                    context.DeserializeTicket(refreshToken.ProtectedTicket);
                    var result = userService.RemoveRefreshToken(hashedTokenId);
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}