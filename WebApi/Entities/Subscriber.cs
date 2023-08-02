using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class Subscriber
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int PhoneNumber  { get; set; }
    }
}
