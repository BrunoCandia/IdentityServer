using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Client
{
    public class Program
    {
        //Forma1 - Expression method
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        //Forma2
        //public static void Main(string[] args)
        //{
        //    MainAsync().GetAwaiter().GetResult();
        //}

        private static async Task MainAsync()
        {
            //Step1 - Get the token
            //var discoveryClient = await DiscoveryClient.GetAsync("http://localhost:5000");            

            //if (discoveryClient.IsError)
            //{
            //    throw new Exception("Error");
            //}

            //var tokenClient = new TokenClient(discoveryClient.TokenEndpoint, "clientId1", "secret");
            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("resourceApi1");

            //if (tokenResponse.IsError)
            //{
            //    throw new Exception("Error");
            //}

            //var tokenJson = tokenResponse.Json;

            var discoveryClient = new HttpClient();
            var disco = await discoveryClient.GetDiscoveryDocumentAsync("http://localhost:5000");

            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }

            var tokenResponse = await discoveryClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "clientId1",
                    ClientSecret = "secret",
                    //ClientSecret = "secret".ToSha256(),
                    Scope = "resourceApi1"
                });

            //Step2 - Make the call to the API 
            var client = new HttpClient();

            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/api/values");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error");
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
            }
        }

        //public static async void Main(string[] args)
        //{

        //}
    }
}
