namespace UrlShortenerApp.Domain.Entities
{
    public class ShortenedUrl
    {
        public int Id { get; set; }
        public string LongUrl { get; set; } 
        public string ShortUrl { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Description { get; set; }
        public int HitCount { get; set; } = 0;
        public string CustomAlias { get; set; }
    }
}
