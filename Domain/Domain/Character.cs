using System;
using Domain.Enums;

namespace Domain.Domain
{
    public class Character
    {
        public Character(
            string characterName,
            CharacterClass cClass,
            int level,
            Guid userId,
            int guildRank)
        {
            this.CharacterName = characterName;
            this.Class = cClass;
            this.Level = level;
            this.UserId = userId;
            this.GuildRank = guildRank;
            this.PublicId = new Guid();
        }
        
        public int Id { get; protected set; }
        
        public string CharacterName { get; protected set; }
        
        public CharacterClass Class { get; protected set; }
        
        public int Level { get; protected set; }
        
        public Guid UserId { get; protected set; }
        
        public Guid PublicId { get; protected set; }
        
        public int GuildRank { get; protected set; }
    }
}