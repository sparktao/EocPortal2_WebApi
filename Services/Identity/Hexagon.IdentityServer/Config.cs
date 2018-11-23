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
                new IdentityResources.Email(),
                new IdentityResource("dataeventrecordsscope",new []{ "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin" , "dataEventRecords.user" } )
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("dataEventRecords")
                {
                    ApiSecrets =
                    {
                        new Secret("dataEventRecordsSecret".Sha256())
                    },
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "dataeventrecordsscope1",
                            DisplayName = "Scope for the dataEventRecords ApiResource"
                        }
                    },
                    UserClaims = { "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin", "dataEventRecords.user" }
                }
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "anotherWebMvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("dataEventRecordsSecret".Sha256())
                    },

                    RedirectUris = { string.Format("{0}/signin-oidc", configuration.GetValue<string>("AnotherWebMvc")) },
                    PostLogoutRedirectUris = { string.Format("{0}/signout-callback-oidc", configuration.GetValue<string>("AnotherWebMvc")) },
                    //OpenId, Profile 准许页面可以通过设置false去除
                    RequireConsent = true,

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "dataEventRecords"
                    },
                    AllowOfflineAccess = true
                },
                new Client
                {
                    ClientId = "resourceownerclient",
                    ClientName = "WebEoc",

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600,
                    IdentityTokenLifetime = 3600,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    SlidingRefreshTokenLifetime = 30,
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AlwaysSendClientClaims = true,
                    Enabled = true,
                    ClientSecrets=  new List<Secret> { new Secret("dataEventRecordsSecret".Sha256()) },

                    RedirectUris = { string.Format("{0}/signin-oidc", configuration.GetValue<string>("WebEoc")) },
                    PostLogoutRedirectUris = { string.Format("{0}/signout-callback-oidc", configuration.GetValue<string>("WebEoc")) },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId, 
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "dataEventRecords"
                    }
                }
            };
        }
    }
}