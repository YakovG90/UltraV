using Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Mapping
{
    public class CharacterMap : DbEntityConfiguration<Character>
    {
        private readonly string schema;

        public CharacterMap(string schema)
        {
            this.schema = schema;
        }
        
        public override void Configure(EntityTypeBuilder<Character> entity)
        {
            entity.ToTable("Character", this.schema);
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
            entity.Property(t => t.CharacterName).HasColumnName("CharacterName").IsRequired();
            entity.Property(t => t.Class).HasColumnName("Class").IsRequired();
            entity.Property(t => t.GuildRank).HasColumnName("GuildRank").IsRequired();
            entity.Property(t => t.Level).HasColumnName("Level").IsRequired();
            entity.Property(t => t.PublicId).HasColumnName("PublicId").IsRequired();
            entity.Property(t => t.UserId).HasColumnName("UserId").IsRequired();
        }
    }
}