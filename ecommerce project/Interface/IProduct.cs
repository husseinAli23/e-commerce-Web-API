using ecommerce_project.Models;

namespace ecommerce_project.Interface;

public interface IProduct
{
    public ICollection<Product> GetProducts(String sortBy = "Ace");
    public Product GetProductById(int id);
    bool ProductExists(int id);
    bool CreateProduct(Product product);
    bool UpdateProduct(Product product);
    bool Save();
}
