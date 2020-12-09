
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Cors;

namespace RestApiSocialFilm.UserAuthentication
{

    public class ApplicationAuthProvider : OAuthAuthorizationServerProvider
    {
      
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            Console.WriteLine("We are in the start of application auth");
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            AuthRepository authRepository = new AuthRepository();
            bool Valid = authRepository.ValidateUser(context.UserName,
            context.Password);
            if (Valid)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("Username", context.UserName));
                identity.AddClaim(new Claim("Password", context.Password));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
        }
    }
}
    