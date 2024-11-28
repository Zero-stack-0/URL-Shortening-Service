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
    }
}