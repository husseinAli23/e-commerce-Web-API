namespace ecommerce_project.Models
{
    public class Cart_item
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int ProductId { get; set; }
        public int Qunatity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
