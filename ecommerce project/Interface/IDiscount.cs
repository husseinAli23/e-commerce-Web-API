using ecommerce_project.Models;

namespace ecommerce_project.Interface
{
    public interface IDiscount
    {
        public ICollection<Discount> GetDiscounts();
        public Discount GetDiscount(int id);
        bool DiscountExists(int id);
        bool CreateDiscount(Discount discount);
        bool Save();
    }
}
