using ecommerce_project.Data;
using ecommerce_project.Interface;
using ecommerce_project.Models;

namespace ecommerce_project.Repository;

public class DiscountRepository : IDiscount
{
    private readonly DatabaseContext _context;

    public DiscountRepository(DatabaseContext databaseContext)
    {
        _context = databaseContext;
    }
    public bool CreateDiscount(Discount discount)
    {
        _context.Add(discount);
        return Save();
    }

    public bool DiscountExists(int id)
    {
       return _context.Discounts.Any(d => d.Id == id);
    }

    public ICollection<Discount> GetDiscounts()
    {
        return _context.Discounts.ToList();
    }

    public Discount GetDiscount(int id)
    {
        return _context.Discounts.Where(d => d.Id == id).FirstOrDefault();
    }

    public bool Save()
    {
       var saved  = _context.SaveChanges();
       return saved > 0 ? true : false;
    }
}
