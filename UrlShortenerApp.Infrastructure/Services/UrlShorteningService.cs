using UrlShortenerApp.Application.Common.Interfaces;
using UrlShortenerApp.Application.Services.Interfaces;
using UrlShortenerApp.Domain.Entities;

namespace UrlShortenerApp.Infrastructure.Services
{
    public class UrlShorteningService : IUrlShorteningService
    {
        // inject IShortenedUrlRepository
        private readonly IShortenedUrlRepository _repository;

        public UrlShorteningService(IShortenedUrlRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> ResolveUrl(string shortUrl)
        {
            var shortenedUrl = await _repository.GetByShortUrl(shortUrl);
            if (shortenedUrl == null || shortenedUrl.ExpirationDate < DateTime.UtcNow)
            {
                return "This link has expired or does not exist.";
            }

            return shortenedUrl.LongUrl;
        }

        public async Task<string> ShortenUrl(string longUrl)
        {
            var shortUrl = GenerateShortUrl();
            var shortenedUrl = new ShortenedUrl()
            {
                CreatedAt = DateTime.UtcNow,
                LongUrl = longUrl,
                ShortUrl = shortUrl,
                ExpirationDate = DateTime.UtcNow.AddHours(12)
            };

            await _repository.Create(shortenedUrl);
            return shortUrl;
        }

        #region Private Methods
        private string GenerateShortUrl()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }
        #endregion
    }
}
