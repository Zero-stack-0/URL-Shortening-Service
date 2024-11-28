using Data.Repository.Interface;
using Entities.Model;
using Service.Dto;
using Service.Interface;
using Microsoft.AspNetCore.Http;

namespace Service;

public class UrlShortService : IUrlShortService
{
    private readonly IUrlRecordRepository urlRecordRepository;
    public UrlShortService(IUrlRecordRepository urlRecordRepository)
    {
        this.urlRecordRepository = urlRecordRepository;
    }

    public async Task<ResponseModel> Add(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return GetResponse(null, StatusCodes.Status400BadRequest, "Url cannot be empty");
        }

        //check if url already exists
        var doesUrlAlreadyExist = await urlRecordRepository.GetByUrl(url);
        if (doesUrlAlreadyExist is not null)
        {
            return GetResponse(doesUrlAlreadyExist, StatusCodes.Status400BadRequest, "Url already exists");
        }

        var shortCode = GetShortCode();
        var doesShortCodeExists = await urlRecordRepository.Get(shortCode);

        //if shortCode already exists in database then create new one
        while (doesShortCodeExists is not null)
        {
            shortCode = GetShortCode();
            doesShortCodeExists = await urlRecordRepository.Get(shortCode);
        }

        var urlShortneningRecord = new UrlShortneningRecord(url, shortCode);

        await urlRecordRepository.Add(urlShortneningRecord);

        return GetResponse(urlShortneningRecord, StatusCodes.Status201Created, "Url added succesfully");
    }

    private string GetShortCode()
    {
        return Guid.NewGuid().ToString().Substring(0, 6); ;
    }

    private ResponseModel GetResponse(UrlShortneningRecord? record, int statusCodes, string message)
    {
        return new ResponseModel()
        {
            Data = record is null ? null : new UrlShortResponse()
            {
                Id = record.Id,
                Url = record.Url,
                CreatedDate = record.CreateDate,
                UpdatedDate = record.UpdatedDate,
                ShortCode = record.ShortCode
            },
            StatusCode = statusCodes,
            Message = message
        };
    }
}