using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using UrlShortenerApp.Domain.Entities;

namespace UrlShortenerApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<ShortenedUrl> ShortenedURLs { get; set; }
    }
}
