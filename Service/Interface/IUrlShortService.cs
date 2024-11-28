using Service.Dto;

namespace Service.Interface
{
    public interface IUrlShortService
    {
        Task<ResponseModel> Add(string url);
        Task<ResponseModel> GetByShortCode(string shortCode);
        Task<ResponseModel> Update(string shortCode, string updatedUrl);
        Task<ResponseModel> Delete(string shortCode);
        Task<ResponseModel> GetStats(string shortCode);
    }
}