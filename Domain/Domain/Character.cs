using System.Collections.Generic;
using System.Linq;

namespace Domain.Domain
{
    using System;
    using Enums;

    public class Character
    {
        public Character(
            string characterName,
            CharacterClass characterClass,
            int level,
            int guildRank,
            int itemLevel,
            string role,
            string specialization,
            int achievementPoints,
            string characterPicture,
            byte[] pictureData)
        {
            this.CharacterName = characterName;
            this.CharacterClass = characterClass;
            //this.Rankings = new List<Ranking>();
            this.Level = level;
            this.GuildRank = guildRank;
            this.PublicId = Guid.NewGuid();
            this.ItemLevel = itemLevel;
            this.Role = role;
            this.Specialization = specialization;
            this.AchievementPoints = achievementPoints;
            this.CharacterPicture = characterPicture;
            this.PictureData = pictureData;
        }

        public int Id { get; protected set; }

        public string CharacterName { get; protected set; }

        public CharacterClass CharacterClass { get; protected set; }

        //public List<Ranking> Rankings { get; protected set; }

        public int Level { get; protected set; }

        public Guid PublicId { get; protected set; }

        public int GuildRank { get; protected set; }

        public int ItemLevel { get; protected set; }

        public string Role { get; protected set; }

        public string Specialization { get; protected set; }

        public int AchievementPoints { get; protected set; }

        public string CharacterPicture{ get; protected set; }

        public byte[] PictureData { get; protected set; }

        /*internal void UpdateRankings(params Ranking[] rankings)
        {
            if (rankings != null)
            {
                this.Rankings = rankings.ToList();
            }
        }*/
    }
}