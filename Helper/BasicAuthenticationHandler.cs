using ApiProject.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace ApiProject.Helper
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ApiContext _context;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock,ApiContext context) : base(options, logger, encoder, clock)
        {
            _context = context;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("No header found");
            }
            var headerVal = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            if (headerVal == null)
            {
                var bytes = Convert.FromBase64String(headerVal.Parameter);
                string credentials=Encoding.UTF8.GetString(bytes);
                string[] array=credentials.Split(':');
                string userEmail = array[0];
                string password = array[1];
                var user=await _context.Users.SingleOrDefaultAsync(u => u.Email == userEmail&& u.Password==password);  
                if (user != null)
                {
                    var claim = new[]
               {
                    new Claim(ClaimTypes.Email, user.Email)
                };
                    var identity = new ClaimsIdentity(claim, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);

                }
                else
                {
                    return AuthenticateResult.Fail("UnAuthorized");
                }
               
            }
            return AuthenticateResult.Fail("Empty Header");
        }
    }
}
