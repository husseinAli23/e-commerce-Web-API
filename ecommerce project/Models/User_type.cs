using System.ComponentModel.DataAnnotations;

namespace ecommerce_project.Models
{
    public class User_type
    {
        public int Id { get; set; }

        [Required, MaxLength(60)]
        public string TypeName { get; set; }
    }
}
