using Data.Repository.Interface;
using Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class UrlRecordRepository : IUrlRecordRepository
    {
        private readonly UrlShortneningDbContext context;
        public UrlRecordRepository(UrlShortneningDbContext context)
        {
            this.context = context;
        }

        public async Task Add(UrlShortneningRecord urlShortneningRecord)
        {
            context.Add(urlShortneningRecord);
            await SaveChanges();
        }

        public async Task Update(UrlShortneningRecord urlShortneningRecord)
        {
            context.Update(urlShortneningRecord);
            await SaveChanges();
        }

        public async Task<UrlShortneningRecord?> Get(string shortCode)
        {
            return await context.UrlShortneningRecord.FirstOrDefaultAsync(it => it.ShortCode == shortCode && !it.IsDeleted);
        }

        public async Task<UrlShortneningRecord?> GetByUrl(string url)
        {
            return await context.UrlShortneningRecord.FirstOrDefaultAsync(it => it.Url == url && !it.IsDeleted);
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}