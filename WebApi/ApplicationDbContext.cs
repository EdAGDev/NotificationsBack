using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<SubscriptionHistory> SubscriptionsHistories { get; set; }
        public DbSet<TypeChannel> TypeChannels { get; set; }
        public DbSet<Channel> Channels { get; set; }
    }
}
