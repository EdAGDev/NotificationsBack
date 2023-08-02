namespace WebApi.Entities
{
    public class SubscriptionHistory
    {
        public int Id { get; set; }
        public int? TypeChannelId { get; set; }
        public int? SubscriberId { get; set; }
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public int Status { get; set; }
    }
}
