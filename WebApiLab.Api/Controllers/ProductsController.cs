using Microsoft.AspNetCore.Mvc;

using WebApiLab.Bll.Interfaces;
using WebApiLab.Bll.Dtos;

namespace WebApiLab.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/<ProductsController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> Get()
    {
        return await _productService.GetProductsAsync();
    }

    // GET api/<ProductsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        return await _productService.GetProductAsync(id);
    }

    // POST api/<ProductsController>
    [HttpPost]
    public async Task<ActionResult<Product>> Post([FromBody] Product product)
    {
        var created = await _productService.InsertProductAsync(product);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    // PUT api/<ProductsController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> Put(int id, [FromBody] Product product)
    {
        return await _productService.UpdateProductAsync(id, product);
    }

    // DELETE api/<ProductsController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}
