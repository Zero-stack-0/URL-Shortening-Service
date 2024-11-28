using Entities.Model;

namespace Data.Repository.Interface;

public interface IUrlRecordRepository
{
    Task Add(UrlShortneningRecord urlShortneningRecord);
    Task Update(UrlShortneningRecord urlShortneningRecord);
    Task<UrlShortneningRecord?> Get(string shortCode);
    Task<UrlShortneningRecord?> GetByUrl(string url);
}
