namespace ecommerce_project.Models;

public class Discount
{
    public int Id {get; set; }
    public decimal DiscountPercentage { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}
