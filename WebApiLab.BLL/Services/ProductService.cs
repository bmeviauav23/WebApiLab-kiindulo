using AutoMapper;

using WebApiLab.Bll.Interfaces;
using WebApiLab.Dal;
using WebApiLab.Bll.Dtos;
using AutoMapper.QueryableExtensions;
using WebApiLab.Bll.Exceptions;

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

    public List<Product> GetProducts()
    {
        var products = _context.Products
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .ToList();

        return products;
    }

    public Product GetProduct(int productId)
    {
        return _context.Products
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .SingleOrDefault(p => p.Id == productId)
            ?? throw new EntityNotFoundException("Nem található a termék", productId);
    }

    public Product InsertProduct(Product newProduct)
    {
        var efProduct = _mapper.Map<Dal.Entities.Product>(newProduct);
        _context.Products.Add(efProduct);
        _context.SaveChanges();
        return GetProduct(efProduct.Id);
    }

    public Product UpdateProduct(int productId, Product updatedProduct)
    {
        var efProduct = _context.Products.SingleOrDefault(p => p.Id == productId)
            ?? throw new EntityNotFoundException("Nem található a termék", productId);
        _mapper.Map(updatedProduct, efProduct);
        _context.SaveChanges();
        return GetProduct(efProduct.Id);
    }

    public void DeleteProduct(int productId)
    {
        var efProduct = _context.Products.SingleOrDefault(p => p.Id == productId)
            ?? throw new EntityNotFoundException("Nem található a termék", productId);
        _context.Products.Remove(efProduct);
        _context.SaveChanges();
    }
}
