﻿using System.Security.Claims;

namespace Authentication.API.Helpers
{
    public static class UserExtensions
    {
        public static string GetActiveUserId(this ClaimsPrincipal principal)
        {
            var userId = principal.Identities.FirstOrDefault()?.Claims.Where(_ => _.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            return userId?.Value;
        }
    }
}
