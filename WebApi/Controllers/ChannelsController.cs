using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChannelsController
    {
        private readonly ChannelsServices _service;

        public ChannelsController(ChannelsServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<AllChannels>>> GetChannels()
        {
            return await _service.GetChannels();
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ResponseChannelSubcription>>> GetTypesChannels()
        {
            return await _service.GetTypesChannels();
        }

        [HttpPut]
        public async Task UpdateSubcription(RequestSubcriptionHistory request)
        {
            await _service.UpdateSubcriptions(request);
        }
    }
}
