using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain
{
    public abstract class DbEntityConfiguration<TEntity>
        where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}