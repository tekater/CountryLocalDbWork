using Microsoft.EntityFrameworkCore;

namespace CountryLocalDbWork
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            string connectionString = @"
            User Id=postgres.clamkmdzzkyenqphuadp;
            Password=AxxHsyUKptHfoxEi;
            Server=aws-0-us-east-1.pooler.supabase.com;
            Port=5432;
            Database=postgres;
            ";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
