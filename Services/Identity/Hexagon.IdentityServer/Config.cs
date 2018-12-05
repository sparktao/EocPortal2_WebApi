// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using System.Collections.Generic;

namespace Hexagon.IdentityServer
{
    using IdentityServer4;
    using Microsoft.Extensions.Configuration;
    using System.Security.Claims;

    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("restapi", "my resetful api")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "restapi" }
                },
                new Client
                {
                    ClientId = "mvcclient",
                    ClientName = "MVC Client",
                    
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("2ac6982f-d0ce-4a51-8f58-48e817447c92".Sha256())
                    },

                    RedirectUris = { string.Format("{0}/signin-oidc", configuration.GetValue<string>("mvcclient")) },
                    //FrontChannelLogoutUri =  string.Format("{0}/signout-oidc", configuration.GetValue<string>("mvcclient")) ,
                    PostLogoutRedirectUris = { string.Format("{0}/signout-callback-oidc", configuration.GetValue<string>("mvcclient")) },
                    //OpenId, Profile 准许页面可以通过设置false去除
                    RequireConsent = true,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "restapi"
                    },
                    AllowOfflineAccess = true
                },

                // SPA client using implicit flow
                new Client
                {
                    ClientId = "ng-client",
                    ClientName = "Angular Client",
                    ClientUri = configuration.GetValue<string>("ngclient"),

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AccessTokenLifetime = 180,

                    RedirectUris =
                    {
                        string.Format("{0}/signin-oidc", configuration.GetValue<string>("ngclient")),
                        string.Format("{0}/redirect-silentrenew", configuration.GetValue<string>("ngclient"))
                    },

                    PostLogoutRedirectUris = { configuration.GetValue<string>("ngclient") },
                    AllowedCorsOrigins = { configuration.GetValue<string>("ngclient") },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "restapi"
                    }
                }
            };
        }
    }
}