using System;
using System.IO;
using System.Net;
using AutoMapper;
using Domain.Domain;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Domain.Services.Implementation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using DataAccess;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Configuration;
    using Models;
    using Newtonsoft.Json;
    using Settings;

    public class CharacterService : ICharacterService
    {
        private readonly string blizzBaseUrl = "https://eu.api.blizzard.com/";

        private readonly ApplicationDbContext context;

        private readonly ServiceAttribute serviceAttribute;

        private readonly ServicesSettings servicesSettings;

        private readonly GenerateToken tokenGenerator;

        private readonly IHttpClientFactory clientFactory;

        private readonly IMapper mapper;

        public IConfiguration Configuration { get; }

        public CharacterService(
            DataContext context,
            IConfiguration configuration,
            GenerateToken tokenGenerator,
            IHttpClientFactory clientFactory,
            IMapper mapper)
        {
            this.context = context.DbContext;
            this.Configuration = configuration;
            this.tokenGenerator = tokenGenerator;
            this.clientFactory = clientFactory;
            this.mapper = mapper;
        }

        public async Task<GuildViewModel> GetGuildMembers()
        {
            var accessToken = await this.tokenGenerator.Generate();
            var url = $"{this.blizzBaseUrl}wow/guild/blackhand/ultraviolet";
            var client = this.clientFactory.CreateClient();
            var fields = new Dictionary<string, string>
            {
                {"fields", "members"}
            };

            var itemField = new Dictionary<string, string>
            {
                {"fields", "items"}
            };

            var urlWithQueries = QueryHelpers.AddQueryString(url, fields);

            var request = new HttpRequestMessage(HttpMethod.Get, $"{urlWithQueries}");

            request.Headers.Add("Authorization", $"Bearer {accessToken}");

            var response = client.SendAsync(request);
            var content = await response.Result.Content.ReadAsStringAsync();
            var guild = JsonConvert.DeserializeObject<GuildViewModel>(content);
            var characterList = new List<Character>();

            var existingMembers = await this.context.Characters
                .Where(m => guild.Members.Any(t => t.Character.Name.Equals(m.CharacterName)))
                .ToListAsync();

            foreach (var member in guild.Members.Where(m => m.Rank <= 2))
            {
                if (existingMembers.Any(m => m.CharacterName.Equals(member.Character.Name)))
                {
                    continue;
                }

                var profileUrl = $"{this.blizzBaseUrl}/wow/character/blackhand/{member.Character.Name}";
                var profileUrlWithQuery = QueryHelpers.AddQueryString(profileUrl, itemField);
                var ilvlRequest = new HttpRequestMessage(HttpMethod.Get, profileUrlWithQuery);
                ilvlRequest.Headers.Add("Authorization", $"Bearer {accessToken}");
                var ilvlResponse = client.SendAsync(ilvlRequest);
                var ilvlContent = await ilvlResponse.Result.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(ilvlContent);

                member.Character.Items.AverageItemLevelEquipped = Convert.ToInt32(data.items.averageItemLevelEquipped);
                var classString = (string) Convert.ToString(data.@class);
                var pictureUrl = (string) Convert.ToString(data.thumbnail);

                member.Character.PictureUrl = pictureUrl.Replace("-avatar.jpg", string.Empty);

                Enum.TryParse<CharacterClass>(
                    classString,
                    out var characterEnum);

                member.Character.CharacterClass = characterEnum;

                var pictureData = await this.SaveImageToDb(member.Character.PictureUrl);

                var character = new Character(
                    member.Character.Name,
                    characterEnum,
                    member.Character.Level,
                    member.Rank,
                    member.Character.Items.AverageItemLevelEquipped,
                    member.Character.Spec.Role,
                    member.Character.Spec.Name,
                    member.Character.AchievementPoints,
                    member.Character.PictureUrl,
                    pictureData);

                characterList.Add(character);
            }

            await this.context.AddRangeAsync(characterList);
            await this.context.SaveChangesAsync();

            return guild;
        }

        public async Task<string> GetMemberPicture(Guid publicId)
        {
            var character = await this.context.Characters
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.PublicId.Equals(publicId));

            return Convert.ToBase64String(character.PictureData);
        }

        public async Task<List<CharacterViewModel>> GetAllGuildMembers()
        {
            var characters = await this.context.Characters
                .AsNoTracking()
                .ToListAsync();

            var characterViewModelList = this.mapper.Map<List<CharacterViewModel>>(characters);

            foreach (var characterViewModel in characterViewModelList)
            {
                characterViewModel.Image =
                    Convert.ToBase64String(
                        characters.FirstOrDefault(c => c.PublicId.Equals(characterViewModel.PublicId))?.PictureData);
            }

            return characterViewModelList;
        }

        private async Task<byte[]> SaveImageToDb(string characterLink)
        {
            var url = $"https://render-eu.worldofwarcraft.com/character/{characterLink}-main.jpg";

            var webClient = new WebClient();
            var imageData = await webClient.DownloadDataTaskAsync(url);

            return imageData;
        }
    }
}