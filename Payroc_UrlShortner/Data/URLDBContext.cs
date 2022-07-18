using Microsoft.EntityFrameworkCore;

namespace Payroc_UrlShortner.Data;

public class URLDBContext : DbContext
{
    public URLDBContext(DbContextOptions<URLDBContext> options)
        : base(options)
    {
        var d = new DbContextOptionsBuilder<URLDBContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;
    }

    public DbSet<Url> Urls { get; set; }
}