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

    public async Task<ResponseModel> GetByShortCode(string shortCode)
    {
        if (string.IsNullOrWhiteSpace(shortCode))
        {
            return GetResponse(null, StatusCodes.Status400BadRequest, "short code cannot be empty");
        }

        var recordByShortCode = await urlRecordRepository.Get(shortCode);
        if (recordByShortCode is null)
        {
            return GetResponse(null, StatusCodes.Status404NotFound, "no url exists with given short code");
        }

        recordByShortCode.Accessed();

        await urlRecordRepository.Update(recordByShortCode);

        return GetResponse(recordByShortCode, StatusCodes.Status200OK, "Record");
    }

    public async Task<ResponseModel> Update(string shortCode, string updatedUrl)
    {
        if (string.IsNullOrWhiteSpace(shortCode) || string.IsNullOrWhiteSpace(updatedUrl))
        {
            return GetResponse(null, StatusCodes.Status400BadRequest, string.IsNullOrWhiteSpace(shortCode) ? "Short code cannot be empty" : "Url cannot be empty");
        }

        var recordByShortCode = await urlRecordRepository.Get(shortCode);
        if (recordByShortCode is null)
        {
            return GetResponse(null, StatusCodes.Status404NotFound, "no url exists with given short code");
        }

        var recordByUrl = await urlRecordRepository.GetByUrl(updatedUrl);
        if (recordByUrl is not null)
        {
            return GetResponse(null, StatusCodes.Status404NotFound, "updated url already beign used with short code");
        }

        recordByShortCode.Update(updatedUrl);

        await urlRecordRepository.Update(recordByShortCode);

        return GetResponse(recordByShortCode, StatusCodes.Status200OK, "updated sucessfully");
    }

    public async Task<ResponseModel> Delete(string shortCode)
    {
        var recordByShortCode = await urlRecordRepository.Get(shortCode);
        if (recordByShortCode is null)
        {
            return GetResponse(null, StatusCodes.Status404NotFound, "no url exists with given short code");
        }

        recordByShortCode.Delete();

        await urlRecordRepository.Update(recordByShortCode);

        return GetResponse(null, StatusCodes.Status204NoContent, "");
    }

    public async Task<ResponseModel> GetStats(string shortCode)
    {
        if (string.IsNullOrWhiteSpace(shortCode))
        {
            return GetResponse(null, StatusCodes.Status400BadRequest, "short code cannot be empty");
        }

        var recordByShortCode = await urlRecordRepository.Get(shortCode);
        if (recordByShortCode is null)
        {
            return GetResponse(null, StatusCodes.Status404NotFound, "no url exists with given short code");
        }

        recordByShortCode.Accessed();

        await urlRecordRepository.Update(recordByShortCode);

        return GetResponse(recordByShortCode, StatusCodes.Status200OK, "Record", true);
    }

    private string GetShortCode()
    {
        return Guid.NewGuid().ToString().Substring(0, 6); ;
    }

    private ResponseModel GetResponse(UrlShortneningRecord? record, int statusCode, string message, bool includeAccessCount = false)
    {
        if (record is null)
        {
            return new ResponseModel
            {
                Data = null,
                StatusCode = statusCode,
                Message = message
            };
        }

        var responseData = new
        {
            record.Id,
            record.Url,
            record.CreateDate,
            record.UpdatedDate,
            record.ShortCode,
            AccessCount = includeAccessCount ? record.AccessCount : (int?)null
        };

        return new ResponseModel
        {
            Data = responseData,
            StatusCode = statusCode,
            Message = message
        };
    }
}