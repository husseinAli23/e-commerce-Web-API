using System.ComponentModel.DataAnnotations;

namespace ecommerce_project.Models
{
    public class Product_category
    {
        public int Id { get; set; }
        [MaxLength(80)]
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
