using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.DataAccess;
using Domain.Settings;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace Domain.Services.Implementation
{
    public class CharacterService : ICharacterService
    {
        private readonly string blizzBaseUrl = "https://eu.api.blizzard.com/";

        private readonly ApplicationDbContext context;

        private readonly ServiceAttribute serviceAttribute;

        private readonly ServicesSettings servicesSettings;

        private readonly GenerateToken tokenGenerator;

        private readonly IHttpClientFactory clientFactory;
        
        public IConfiguration Configuration { get; }

        public CharacterService(
            DataContext context,
            IConfiguration configuration,
            GenerateToken tokenGenerator,
            IHttpClientFactory clientFactory)
        {
            this.context = context.DbContext;
            this.Configuration = configuration;
            this.tokenGenerator = tokenGenerator;
            this.clientFactory = clientFactory;
        }

        public async Task<string> GetGuildMembers()
        {
            var accessToken = await this.tokenGenerator.Generate();

            var url = $"{blizzBaseUrl}wow/guild/blackhand/ultraviolet";

            var client = this.clientFactory.CreateClient();

            var fields = new Dictionary<string, string>
            {
                { "fields", "members" }
            };

            var urlWithQueries = QueryHelpers.AddQueryString(url, fields);

            var request = new HttpRequestMessage(HttpMethod.Get, $"{urlWithQueries}");
            
            request.Headers.Add("Authorization", $"Bearer {accessToken}");

            var response = client.SendAsync(request);

            var content = await response.Result.Content.ReadAsStringAsync();

            return content;
        }
    }
}