using AutoMapper;

using WebApiLab.Bll.Interfaces;
using WebApiLab.Dal;
using WebApiLab.Bll.Dtos;
using AutoMapper.QueryableExtensions;
using WebApiLab.Bll.Exceptions;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Product>> GetProductsAsync()
    {
        var products = await _context.Products
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return products;
    }

    public async Task<Product> GetProductAsync(int productId)
    {
        return await _context.Products
            .ProjectTo<Product>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(p => p.Id == productId)
            ?? throw new EntityNotFoundException("Nem található a termék", productId);
    }

    public async Task<Product> InsertProductAsync(Product newProduct)
    {
        var efProduct = _mapper.Map<Dal.Entities.Product>(newProduct);
        _context.Products.Add(efProduct);
        _context.SaveChanges();
        return await GetProductAsync(efProduct.Id);
    }

    public async Task<Product> UpdateProductAsync(int productId, Product updatedProduct)
    {
        var efProduct = await _context.Products.SingleOrDefaultAsync(p => p.Id == productId)
            ?? throw new EntityNotFoundException("Nem található a termék", productId);
        _mapper.Map(updatedProduct, efProduct);
        await _context.SaveChangesAsync();
        return await GetProductAsync(efProduct.Id);
    }

    public async Task DeleteProductAsync(int productId)
    {
        var efProduct = await _context.Products.SingleOrDefaultAsync(p => p.Id == productId)
            ?? throw new EntityNotFoundException("Nem található a termék", productId);
        _context.Products.Remove(efProduct);
        await _context.SaveChangesAsync();
    }
}
