using Entities.Model;
using Microsoft.EntityFrameworkCore;
namespace Data
{
    public class UrlShortneningDbContext : DbContext
    {
        public UrlShortneningDbContext()
        { }

        public UrlShortneningDbContext(DbContextOptions<UrlShortneningDbContext> options) : base(options)
        {

        }

        public DbSet<UrlShortneningRecord> UrlShortneningRecord { get; set; }
    }
}