using Service.Dto;

namespace Service.Interface
{
    public interface IUrlShortService
    {
        Task<ResponseModel> Add(string url);
    }
}