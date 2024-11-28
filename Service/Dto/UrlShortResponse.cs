namespace Service.Dto
{
    public class UrlShortResponse
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public string ShortCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}