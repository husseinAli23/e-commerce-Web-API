using System.ComponentModel.DataAnnotations;

namespace ecommerce_project.Dto
{
    public class CreateProductDTO
    {
      
        [MaxLength(120, ErrorMessage = "The Name should be less then 120 char")]
        public string Name { get; set; }
        public string Details { get; set; }
        public int CategoryId { get; set; }
        public string ImageTitle { get; set; }
        public byte[]? ImageData { get; set; }
        public decimal Price { get; set; }
        public int? DiscountId { get; set; }
        public bool Status { get; set; }
    }
}
