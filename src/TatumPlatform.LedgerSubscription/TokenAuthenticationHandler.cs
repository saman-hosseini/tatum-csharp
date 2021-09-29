using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TatumPlatform.LedgerSubscription
{
    public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationOptions>
    {
        public IServiceProvider ServiceProvider { get; set; }

        public TokenAuthenticationHandler(IOptionsMonitor<TokenAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IServiceProvider serviceProvider)
            : base(options, logger, encoder, clock)
        {
            ServiceProvider = serviceProvider;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var headers = Request.Headers;
            StringValues hmacDigest = "";
            headers.TryGetValue("x-payload-hash", out hmacDigest);

            if (string.IsNullOrEmpty(hmacDigest))
            {
                return Task.FromResult(AuthenticateResult.Fail("Token is null"));
            }

            bool isValidToken = false; // check token here

            if (!isValidToken)
            {
                return Task.FromResult(AuthenticateResult.Fail($"Balancer not authorize token : for token={hmacDigest}"));
            }

            var claims = new[] { new Claim("token", hmacDigest) };
            var identity = new ClaimsIdentity(claims, nameof(TokenAuthenticationHandler));
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), this.Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

    public static class SchemesNamesConst
    {
        public const string TokenAuthenticationDefaultScheme = "TokenAuthenticationScheme";
    }

    public class TokenAuthenticationOptions : AuthenticationSchemeOptions
    {
        public Regex PhoneMask { get; set; }// = new Regex("7\\d{10}");
    }
}
