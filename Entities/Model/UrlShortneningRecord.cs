namespace Entities.Model
{
    public class UrlShortneningRecord
    {
        //Ef core
        public UrlShortneningRecord()
        { }

        public UrlShortneningRecord(string url, string shortCode)
        {
            Url = url;
            ShortCode = shortCode;
            CreateDate = DateTime.UtcNow;
            IsDeleted = false;
        }

        public long Id { get; set; }
        public string Url { get; set; }
        public string ShortCode { get; set; }
        public int AccessCount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public void Accessed()
        {
            AccessCount += 1;
        }
    }
}