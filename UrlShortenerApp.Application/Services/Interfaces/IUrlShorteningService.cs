namespace UrlShortenerApp.Application.Services.Interfaces
{
    public interface IUrlShorteningService
    {
        Task<string> ShortenUrl(string longUrl);
        Task<string> ResolveUrl(string shortUrl);
    }
}
