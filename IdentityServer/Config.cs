using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>
            {
                new Client
                {
                    ClientId = "clientId1",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        //new Secret("secret".Sha256())
                        new Secret("secret")
                    },
                    AllowedScopes = {
                        "resourceApi1"
                    }
                },
                new Client
                {
                    ClientId = "clientId2",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret2".Sha256())
                    },
                    AllowedScopes = {
                        "resourceApi1"
                    }
                }
            };

            return clients;
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            var resources = new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "resourceApi1",
                    DisplayName = "My resource"                    
                }
            };

            return resources;
        }
    }
}
