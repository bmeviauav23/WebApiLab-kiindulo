﻿using Microsoft.AspNetCore.Mvc;
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
    public IEnumerable<Product> Get()
    {
        return _productService.GetProducts();
    }

    // GET api/<ProductsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<ProductsController>
    [HttpPost]
    public ActionResult<Product> Post([FromBody] Product product)
    {
        var p = _productService.InsertProduct(product);
        return CreatedAtAction(nameof(Get), new { id = p.Id }, p);
    }

    // PUT api/<ProductsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ProductsController>/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _productService.DeleteProduct(id);
        return NoContent();
    }
}
