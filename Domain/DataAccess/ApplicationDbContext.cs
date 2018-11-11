namespace Domain.DataAccess
{
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        private const string ConnectionString =
            "Data Source=(LocalDb)\\MSSQLLocalDB;database=UltraViolet.Local;Trusted_Connection=True;";

        private readonly string schema;

        public ApplicationDbContext()
            : this(new DbContextOptionsBuilder().UseSqlServer(ConnectionString).Options, "ultra")
        {
        }

        public ApplicationDbContext(DbContextOptions options, string schema)
            : base(options)
        {
            this.schema = schema;
        }
        
        public virtual DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(this.schema);
        }
    }
}