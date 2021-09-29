using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TatumPlatform.LedgerSubscription
{
    public class CheckHmacDigestHandler : AuthorizationHandler<HmacDigest>
    {
        public CheckHmacDigestHandler()
        {

        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, HmacDigest requirement)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                return;
            }
        }
    }

    public class HmacDigest : IAuthorizationRequirement
    {
        public string HmacSecret { get; }

        public HmacDigest(string hmacSecret)
        {
            HmacSecret = hmacSecret;
        }
    }
}

