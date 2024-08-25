using UrlShortenerApp.Application.Common.Models;

namespace UrlShortenerApp.Application.Services.Interfaces
{
    public interface IUrlShorteningService
    {
        Task<string> ShortenUrl(UrlDto urlDto);
        Task<string> ResolveUrl(string shortUrl);
    }
}
