using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TatumPlatform.LedgerSubscription.Model;

namespace TatumPlatform.LedgerSubscription
{
    public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationOptions>
    {
        public IServiceProvider ServiceProvider { get; set; }
        private readonly TokenAuthenticationOptions _options;
        public TokenAuthenticationHandler(IOptionsMonitor<TokenAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IServiceProvider serviceProvider)
            : base(options, logger, encoder, clock)
        {
            ServiceProvider = serviceProvider;
            _options = options.CurrentValue;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var headers = Request.Headers;
            headers.TryGetValue("x-payload-hash", out StringValues hmacDigest);

            if (string.IsNullOrEmpty(hmacDigest))
            {
                return await Task.FromResult(AuthenticateResult.Fail("Token is null"));
            }

            var ms = new MemoryStream();
            await Request.Body.CopyToAsync(ms);
            ms.Seek(0, SeekOrigin.Begin);
            var body = await new StreamReader(ms).ReadToEndAsync();
            var content = Encoding.UTF8.GetBytes(body);
            ms.Seek(0, SeekOrigin.Begin);
            Request.Body = ms;

            using (var context = new LedgerContext())
            {
                await context.IncomingRequest.AddAsync(new IncomingRequest()
                {
                    JsonData = "x-payload-hash:" + hmacDigest +
                    ",body:" + body
                });
                await context.SaveChangesAsync();
            }

            bool isValidToken = false; // check token here
            var sign = HashHMAC(_options.Secret, body);
            isValidToken = (sign == hmacDigest);
            if (!isValidToken)
            {
                return await Task.FromResult(AuthenticateResult.Fail($"Balancer not authorize token : for token={hmacDigest}"));
            }

            var claims = new[] { new Claim("token", hmacDigest) };
            var identity = new ClaimsIdentity(claims, nameof(TokenAuthenticationHandler));
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), this.Scheme.Name);
            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }

        private static string HashHMAC(string key, string message)
        {
            var secretBye = Encoding.UTF8.GetBytes(key);
            var messageBye = Encoding.UTF8.GetBytes(message);
            var hash = new HMACSHA512(secretBye);
            var result = hash.ComputeHash(messageBye);
            var r = Convert.ToBase64String(result);
            return r;
        }
    }

    public static class SchemesNamesConst
    {
        public const string TokenAuthenticationDefaultScheme = "TokenAuthenticationScheme";
    }

    public class TokenAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string Secret { get; set; } = "1D899DF6-282C-4579-B871-1F918BFFC35F";
    }
}
