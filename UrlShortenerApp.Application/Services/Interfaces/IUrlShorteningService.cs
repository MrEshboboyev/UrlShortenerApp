using UrlShortenerApp.Application.Common.Models;
using UrlShortenerApp.Domain.Entities;

namespace UrlShortenerApp.Application.Services.Interfaces
{
    public interface IUrlShorteningService
    {
        Task<string> ShortenUrl(UrlDto urlDto);
        Task<string> ResolveUrl(string shortUrl);
        Task<IEnumerable<ShortenedUrl>> GetAllURLs();
    }
}
