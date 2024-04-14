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
    public ActionResult<IEnumerable<Product>> Get()
    {
        return _productService.GetProducts();
    }

    // GET api/<ProductsController>/5
    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        return _productService.GetProduct(id);
    }

    // POST api/<ProductsController>
    [HttpPost]
    public ActionResult<Product> Post([FromBody] Product product)
    {
        var created = _productService.InsertProduct(product);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    // PUT api/<ProductsController>/5
    [HttpPut("{id}")]
    public ActionResult<Product> Put(int id, [FromBody] Product product)
    {
        return _productService.UpdateProduct(id, product);
    }

    // DELETE api/<ProductsController>/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _productService.DeleteProduct(id);
        return NoContent();
    }
}
