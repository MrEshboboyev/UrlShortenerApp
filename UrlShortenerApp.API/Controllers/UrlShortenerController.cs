using Microsoft.AspNetCore.Mvc;
using UrlShortenerApp.Application.Common.Models;
using UrlShortenerApp.Application.Services.Interfaces;

namespace UrlShortenerApp.API.Controllers
{
    [Route("")]
    [ApiController]
    public class UrlShortenerController : ControllerBase
    {
        // inject IUrlShorteningService
        private readonly IUrlShorteningService _urlShorteningService;

        public UrlShortenerController(IUrlShorteningService urlShorteningService)
        {
            _urlShorteningService = urlShorteningService;
        }

        [HttpPost]
        [Route("shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] UrlDto urlDto)
        {
            var shortUrl = await _urlShorteningService.ShortenUrl(urlDto);
            return Ok(new { ShortUrl = shortUrl });
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> ResolveUrl(string shortUrl)
        {
            var longUrl = await _urlShorteningService.ResolveUrl(shortUrl);
            return Redirect(longUrl);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllURLs()
        {
            return Ok(await _urlShorteningService.GetAllURLs());
        }
    }
}
