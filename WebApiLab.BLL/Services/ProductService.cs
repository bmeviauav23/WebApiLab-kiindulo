using AutoMapper;

using WebApiLab.Bll.Interfaces;
using WebApiLab.Dal;
using WebApiLab.Bll.Dtos;
using AutoMapper.QueryableExtensions;

namespace WebApiLab.Bll.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProductService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<Product> GetProducts()
    {
        var products = _context.Products
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .AsEnumerable();

        return products;
    }

    public Product GetProduct(int productId)
    {
        throw new NotImplementedException();
    }

    public Product InsertProduct(Product newProduct)
    {
        throw new NotImplementedException();
    }

    public void UpdateProduct(int productId, Product updatedProduct)
    {
        throw new NotImplementedException();
    }

    public void DeleteProduct(int productId)
    {
        throw new NotImplementedException();
    }

}
