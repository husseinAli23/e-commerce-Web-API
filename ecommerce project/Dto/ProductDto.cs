using ecommerce_project.Models;

namespace ecommerce_project.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int CategoryId { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public decimal Price { get; set; }
        public int DiscountId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Rate> Rates { get; set; }

    }
}
