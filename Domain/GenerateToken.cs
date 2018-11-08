using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Settings;

namespace Domain
{
    public class GenerateToken
    {
        private readonly Dictionary<string, AccessTokenModel> cache = new Dictionary<string, AccessTokenModel>();

        private readonly ServiceAttribute serviceAttribute;

        private readonly ServicesSettings servicesSettings;

        private readonly IHttpClientFactory clientFactory;

        public GenerateToken(
            IHttpClientFactory clientFactory,
            ServicesSettings servicesSettings)
        {
            this.clientFactory = clientFactory;
            this.servicesSettings = servicesSettings;
            this.serviceAttribute = servicesSettings.Services
                .FirstOrDefault(s => s.Key.Equals("BlizzardApi", StringComparison.OrdinalIgnoreCase)).Value;
        }

        public async Task<string> Generate()
        {
            var client = this.clientFactory.CreateClient();

            var content = new FormUrlEncodedContent(
                new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", this.serviceAttribute.ServiceClientId),
                    new KeyValuePair<string, string>("client_secret", this.serviceAttribute.ServiceClientSecret)
                });

            var result = await client.PostAsync($"{this.serviceAttribute.ServiceLink}/oauth/token", content);
            var response = await result.Content.ReadAsAsync<AccessTokenModel>();

            return response.AccessToken;
        }
    }
}