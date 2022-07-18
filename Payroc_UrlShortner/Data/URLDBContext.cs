using Microsoft.EntityFrameworkCore;

namespace Payroc_UrlShortner.Data;

public class URLDBContext : DbContext
{
    public URLDBContext(DbContextOptions<URLDBContext> options)
        : base(options)
    {
    }

    public DbSet<Url> Urls { get; set; }
}