using Microsoft.AspNetCore.Authorization;
using System;

namespace TravelAgency.Extensions
{
    public static class AuthorizationOptionsExtensions
    {
        public static AuthorizationOptions AddAzurePolicy(this AuthorizationOptions authorizationOptions, Action<AzureAuth> configureOptions)
        {
            var azureAuth = new AzureAuth();
            configureOptions(azureAuth);

            foreach (var role in azureAuth.Roles)
            {
                authorizationOptions.AddPolicy(role.Name, builder => builder.RequireClaim(role.Claim.Type, role.Claim.Values));
            }

            return authorizationOptions;
        }
    }
}