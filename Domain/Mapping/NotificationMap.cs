namespace Domain.Mapping
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class NotificationMap : DbEntityConfiguration<Notification>
    {
        public string schema;

        public NotificationMap(string schema)
        {
            this.schema = schema;
        }

        public override void Configure(EntityTypeBuilder<Notification> entity)
        {
            entity.ToTable("Notification", this.schema);
            entity.HasKey("Id");

            entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
            entity.Property(t => t.Text).HasColumnName("Text").IsRequired();
            entity.Property(t => t.IsStickied).HasColumnName("IsStickied").IsRequired(false);
            entity.Property(t => t.OrderIndex).HasColumnName("OrderIndex").IsRequired(false);
        }
    }
}