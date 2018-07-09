using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using System.Web.Http;
using ApiContent.DataAccess;
using ApiContent.Models;
using ApiContent.Services;

namespace ApiContent.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserData _userData;
        private readonly CryptoService _cryptoService;

        public SimpleAuthorizationServerProvider()
        { 
            _userData = new UserData();
            _cryptoService = new CryptoService();    
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new [] {"*"});

            UserData _repo = new UserData();
            User user = await _repo.GetUser(context.UserName);
            bool isError = true;
            if (user != null)
            {
                if (_cryptoService.CheckPassword(context.Password, user.Password))
                {
                    isError = false;
                }        
            }
            if (isError)
            {
                context.SetError("invalid grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (context.UserName.Contains("3"))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "User"));
            }
            else
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            }
            context.Validated(identity);
        }

        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            if (context.OwinContext.Request.Method == "OPTIONS" && context.IsTokenEndpoint)
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "POST" });
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "accept", "authorization", "content-type" });
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                context.OwinContext.Response.StatusCode = 200;
                context.RequestCompleted();

                return Task.FromResult<object>(null);
            }

            return base.MatchEndpoint(context);
        }
    }
}