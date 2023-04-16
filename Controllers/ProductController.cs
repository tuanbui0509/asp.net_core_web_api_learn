using System.Net;
using asp.net_core_web_api_learn.Models;
using asp.net_core_web_api_learn.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    [HttpGet]
    [Authorize]
    public ActionResult GetAllProducts(string? search, double? from, double? to, string? sortBy = "productName_desc", int page = 1)
    {
        var result = _productRepository.GetAll(search, from, to, sortBy, page);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult GetById(string id)
    {
        try
        {
            // LINQ [Object] Query
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        catch (System.Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]

    public IActionResult Create([FromBody] ProductVM productVm)
    {
        var product = _productRepository.Add(productVm);
        return Ok(new
        {
            Success = true,
            data = product,
        });
    }

    // PUT api/values/5

    [HttpPut("{id}")]

    public IActionResult Put(string id, [FromBody] Product productEdit)
    {
        try
        {
            _productRepository.Update(productEdit, id);
            return Ok();
        }
        catch (System.Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult Delete(string id)
    {
        try
        {
            _productRepository.Delete(id);
            return Ok("Delete success");
        }
        catch (System.Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

}