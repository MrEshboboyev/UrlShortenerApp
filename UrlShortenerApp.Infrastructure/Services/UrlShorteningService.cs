using Microsoft.Extensions.Configuration;
using UrlShortenerApp.Application.Common.Interfaces;
using UrlShortenerApp.Application.Common.Models;
using UrlShortenerApp.Application.Services.Interfaces;
using UrlShortenerApp.Domain.Entities;

namespace UrlShortenerApp.Infrastructure.Services
{
    public class UrlShorteningService : IUrlShorteningService
    {
        // inject IShortenedUrlRepository, IConfiguration
        private readonly IShortenedUrlRepository _repository;
        private readonly string _baseUrl;

        public UrlShorteningService(IShortenedUrlRepository repository,
            IConfiguration config)
        {
            _repository = repository;
            _baseUrl = config["AppSettings:BaseUrl"];
        }

        public async Task<string> ResolveUrl(string shortUrl)
        {
            // Decode the URL-encoded shortUrl
            var decodedUrl = Uri.UnescapeDataString(shortUrl);

            // Extract the part after the last '/'
            var extractedShortUrl = decodedUrl.Substring(decodedUrl.LastIndexOf('/') + 1);
            var shortenedUrl = await _repository.GetByShortUrl(extractedShortUrl);
            if (shortenedUrl == null || shortenedUrl.ExpirationDate < DateTime.UtcNow)
            {
                return string.Empty;
            }

            return shortenedUrl.LongUrl;
        }

        public async Task<string> ShortenUrl(UrlDto urlDto)
        {
            var shortUrl = GenerateShortUrl();
            var shortenedUrl = new ShortenedUrl()
            {
                CreatedAt = DateTime.UtcNow,
                LongUrl = urlDto.LongUrl,
                Description = urlDto.Description,
                CustomAlias = urlDto.CustomAlias,
                ShortUrl = shortUrl,
                ExpirationDate = DateTime.UtcNow.AddHours(12)
            };

            await _repository.Create(shortenedUrl);


            return $"{_baseUrl}{shortUrl}";
        }

        public async Task<IEnumerable<ShortenedUrl>> GetAllURLs()
        {
            return await _repository.GetAllURLs();
        }

        #region Private Methods
        private string GenerateShortUrl()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }
        #endregion
    }
}
