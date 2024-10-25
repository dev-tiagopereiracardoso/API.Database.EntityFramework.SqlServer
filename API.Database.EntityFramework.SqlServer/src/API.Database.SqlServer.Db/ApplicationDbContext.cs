using API.Database.SqlServer.Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Database.SqlServer.Db
{
    public class ApplicationDbContext : DbContext
    {
        private string _connectionString { set; get; }

        public ApplicationDbContext(
                DbContextOptions<ApplicationDbContext> options, 
                IConfiguration configuration
            ) 
            : base(options) 
        {
            _connectionString = configuration["SqlConnectionString"]!; 
        }

        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {
                var p = item.FindPrimaryKey()!.Properties.FirstOrDefault(i => i.ValueGenerated != Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.Never);
                if (p != null)
                {
                    p.ValueGenerated = Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.Never;
                }
            }
        }
    }
}