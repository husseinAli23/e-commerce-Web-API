using ecommerce_project.Data;
using ecommerce_project.Dto;
using ecommerce_project.Interface;
using ecommerce_project.Models;

namespace ecommerce_project.Repository;

public class CategoryRepository : ICategory
{

    private readonly DatabaseContext _context;

    public CategoryRepository(DatabaseContext Dbcontext)
    {
        _context = Dbcontext;
    }
    public bool CategoryExists(int id)
    {
        return _context.ProductCategories.Any(c => c.Id == id);

    }

    public bool CreateCatagory(Product_category category)
    {
        _context.Add(category);
        return Save();
    }

    public ICollection<Product_category> GetCategories()
    {
        return _context.ProductCategories.ToList();
    }

    public Product_category GetCategory(int id)
    {
        return _context.ProductCategories.Where(c => c.Id == id).SingleOrDefault();
    }

    public bool Save()
    {
        var saved = _context.SaveChanges();
        return saved > 0 ? true : false;
    }

    public bool UpdateCatagory(Product_category category)
    {
        _context.Update(category);
        return Save();
    }
}
