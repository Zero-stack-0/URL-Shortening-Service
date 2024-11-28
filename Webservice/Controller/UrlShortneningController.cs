using Azure;
using Microsoft.AspNetCore.Mvc;
using Service.Dto;
using Service.Interface;

namespace Webservice.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlShortneningController : ControllerBase
    {
        private readonly IUrlShortService urlShortService;
        public UrlShortneningController(IUrlShortService urlShortService)
        {
            this.urlShortService = urlShortService;
        }

        [HttpPost]
        public async Task<ResponseModel> Add([FromBody] ShortUrlRequest request)
        {
            return await urlShortService.Add(request.Url);
        }

        [HttpGet]
        public async Task<ResponseModel> Get([FromQuery] string shortCode)
        {
            return await urlShortService.GetByShortCode(shortCode);
        }

        [HttpPut]
        public async Task<ResponseModel> Update([FromQuery] string shortCode, [FromBody] ShortUrlRequest request)
        {
            return await urlShortService.Update(shortCode, request.Url);
        }

        [HttpDelete]
        public async Task<ResponseModel> Delete([FromQuery] string shortCode)
        {
            return await urlShortService.Delete(shortCode);
        }

        [HttpGet("stats")]
        public async Task<ResponseModel> GetStats([FromQuery] string shortCode)
        {
            return await urlShortService.GetStats(shortCode);
        }
    }
}