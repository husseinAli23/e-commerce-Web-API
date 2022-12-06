using ecommerce_project.Dto;
using ecommerce_project.Models;

namespace ecommerce_project.Interface
{
    public interface ICategory
    {
        public ICollection<Product_category> GetCategories();
        public Product_category GetCategory(int id);
        bool CategoryExists(int id);

        bool CreateCatagory(Product_category category);


        bool UpdateCatagory(Product_category category);
        bool Save();
    }
}
