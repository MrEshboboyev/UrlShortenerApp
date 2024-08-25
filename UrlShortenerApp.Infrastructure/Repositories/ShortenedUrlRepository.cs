using Microsoft.EntityFrameworkCore;
using UrlShortenerApp.Application.Common.Interfaces;
using UrlShortenerApp.Domain.Entities;
using UrlShortenerApp.Infrastructure.Data;

namespace UrlShortenerApp.Infrastructure.Repositories
{
    public class ShortenedUrlRepository : IShortenedUrlRepository
    {
        // inject DI
        private readonly ApplicationDbContext _db;

        public ShortenedUrlRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(ShortenedUrl shortenedUrl)
        {
            _db.ShortenedURLs.Add(shortenedUrl);
            await _db.SaveChangesAsync();
        }

        public async Task<ShortenedUrl> GetByShortUrl(string shortUrl)
        {
            return await _db.ShortenedURLs.SingleOrDefaultAsync(s => s.ShortUrl == shortUrl);
        }

        public async Task<bool> ShortUrlExist(int shortId)
        {
            return await _db.ShortenedURLs.AnyAsync(s => s.Id == shortId);
        }

        public async Task<IEnumerable<ShortenedUrl>> GetAllURLs()
        {
            return await _db.ShortenedURLs.ToListAsync();
        }
    }
}
