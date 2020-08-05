using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop.Web.Security
{
    public static class Clients
    {
        public static Client PasswordClient => new Client()
        {
            ClientId = "PizzaUserClient",
            ClientSecrets = new List<Secret>() { new Secret("secret".Sha256()) },
            AllowAccessToAllScopes = true,
            ClientName = "Pizza Web Client",
            Flow = Flows.ResourceOwner,
            RedirectUris = new List<string>() { "http://localhost:8888", "http://localhost:4200/index.html" }
        };

        public static Client WebClient => new Client()
        {
            ClientId = "PizzaWebClient",
            ClientSecrets = new List<Secret>() { new Secret("secret".Sha256()) },
            AllowAccessToAllScopes = true,
            ClientName = "Pizza Web Client",
            Flow = Flows.AuthorizationCode,
            RedirectUris = new List<string>() {
                "http://localhost:4200" },
            PostLogoutRedirectUris = new List<string>() { "http://localhost:4200" }
        };
    }
}