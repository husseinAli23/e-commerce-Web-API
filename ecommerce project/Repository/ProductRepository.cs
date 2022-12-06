using ecommerce_project.Data;
using ecommerce_project.Interface;
using ecommerce_project.Models;
using System.Linq;

namespace ecommerce_project.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly DatabaseContext _context;

        public ProductRepository(DatabaseContext Dbcontext)
        {
            _context = Dbcontext;
        }

        public bool CreateProduct(Product product)
        {
            _context.Products.Add(product);
            return Save();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Product> GetProducts(String sortBy)
        {
            return _context.Products.OrderBy(p=> p.Price).ToList();
        }

        public bool ProductExists(int id)
        {
           return _context.Products.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return Save();
        }
    }
}
