using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTOs;
using WebApi.Entities;

namespace WebApi.Services
{
    public class ChannelsServices
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ChannelsServices> logger;

        public ChannelsServices(ApplicationDbContext context, ILogger<ChannelsServices> logger)
        {
            _context = context;
            this.logger = logger;
        }

        public async Task<ActionResult<List<AllChannels>>> GetChannels()
        {
            return await (from ch in _context.Channels
                          join tCh in _context.TypeChannels on ch.TypeChannelId equals tCh.Id
                          join sH in _context.SubscriptionsHistories on tCh.Id equals sH.TypeChannelId
                          where sH.Status == 1
                          select new AllChannels()
                          {
                              Id = ch.Id,
                              Name = ch.Name,
                              Image = ch.Image,
                              Type = tCh.Name
                          }).ToListAsync();
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ResponseChannelSubcription>>> GetTypesChannels()
        {
            return await (from sH in _context.SubscriptionsHistories
                          join tCh in _context.TypeChannels
                          on sH.TypeChannelId equals tCh.Id
                          select new ResponseChannelSubcription()
                          {
                              Id = tCh.Id,
                              Name = tCh.Name,
                              Image = tCh.Image,
                              Status = Convert.ToBoolean(sH.Status)
                          }).ToListAsync();
        }

        public async Task UpdateSubcriptions(RequestSubcriptionHistory request)
        {
            logger.LogInformation("Test test");
            var result = await _context.SubscriptionsHistories.Where(x => x.TypeChannelId == request.Id).FirstOrDefaultAsync();
            result.TypeChannelId = result.TypeChannelId;
            result.SubscriberId = result.SubscriberId;
            result.UpdateTime = DateTime.Now;
            result.Status = request.Status ? 1 : 0;
            await _context.SaveChangesAsync();
        }
    }
}
