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
        public async Task<ResponseModel> Shorten([FromBody] ShortUrlRequest request)
        {
            return await urlShortService.Add(request.Url);
        }
    }
}