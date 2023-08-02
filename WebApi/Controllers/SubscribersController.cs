using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTOs;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscribersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubscribersController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Subscriber>>> Get()
        {
            return await _context.Subscribers.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Subscriber subscriber)
        {
            _context.Add(subscriber);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Subscriber subscriber, int id)
        {
            if (subscriber.Id != id)
            {
                return BadRequest("The ID does not match");
            }

            _context.Update(subscriber);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await _context.Subscribers.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return BadRequest("The ID does not exist");
            }

            _context.Remove(new Subscriber() { Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
