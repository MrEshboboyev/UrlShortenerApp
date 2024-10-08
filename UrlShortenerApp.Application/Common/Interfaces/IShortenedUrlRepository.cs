﻿using UrlShortenerApp.Domain.Entities;

namespace UrlShortenerApp.Application.Common.Interfaces
{
    public interface IShortenedUrlRepository
    {
        Task<ShortenedUrl> GetByShortUrl(string shortUrl);
        Task Create(ShortenedUrl shortenedUrl);
        Task<bool> ShortUrlExist(int shortId);
        Task<IEnumerable<ShortenedUrl>> GetAllURLs();
    }
}
