using System.ComponentModel.DataAnnotations;

namespace ecommerce_project.Models
{
    public class User
    {
        public int ID{ get; set; }
        [Required, MaxLength(60)]
        public string Username { get; set; }
        [Required, MaxLength(60)]
        public string Password{ get; set; }
        [Required, MaxLength(90)]
        public string Email { get; set; }
        [Required]
        public int TypeId { get; set; }
        [MaxLength(60)]
        
        public string? FristName { get; set; }
        [MaxLength(60)]
        public string? LastName { get; set; }
        [MaxLength(60)] 
        public string? Address { get; set; }
        [MaxLength(60)] 
        public string? PhoneNumber { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Rate>? Rates{ get; set; }
    }
}
