using WebApiLab.Bll.Dtos;

namespace WebApiLab.Bll.Interfaces;

public interface IProductService
{
    public Product GetProduct(int productId);
    public List<Product> GetProducts();
    public Product InsertProduct(Product newProduct);
    public Product UpdateProduct(int productId, Product updatedProduct);
    public void DeleteProduct(int productId);
}
