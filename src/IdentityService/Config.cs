﻿using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("auctionApp","Auction app full access"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "postman",
                ClientName = "Postman",
                AllowedScopes = { "openid", "profile","auctionApp" },
                RedirectUris = {"https://wwww.getpostman.com/oauth2/callback"},
                ClientSecrets= new[] {new Secret("NotASecret".Sha256())},
                AllowedGrantTypes={GrantType.ResourceOwnerPassword}
               
            },
            new Client
            {
                ClientId = "nextApp",
                ClientName = "nextApp",
                AllowedScopes = { "openid", "profile","auctionApp" },
                RedirectUris = {"http://localhost:3000/api/auth/callback/id-server"},
                ClientSecrets= new[] {new Secret("secret".Sha256())},
                AllowedGrantTypes= GrantTypes.CodeAndClientCredentials,
                RequirePkce = false,
                AllowOfflineAccess=true,
                AllowedScopes = {"openid","profile","auctionApp"}

               
            },

        };
}
