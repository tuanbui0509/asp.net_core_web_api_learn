using asp.net_core_web_api_learn.Model;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    public static List<Product> Products = new List<Product>();

    [HttpGet]
    public ActionResult GetAll()
    {

        return Ok(Products);
    }

    [HttpGet("{id}")]
    public ActionResult GetById(string id)
    {
        try
        {
            // LINQ [Object] Query
            var product = Products.FirstOrDefault(p => p.ProductId == Guid.Parse(id));
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

    [HttpPost]

    public IActionResult Create([FromBody] ProductVM productVm)
    {
        Product product = new Product()
        {
            ProductId = Guid.NewGuid(),
            ProductName = productVm.ProductName,
            ProductPrice = productVm.ProductPrice
        };
        Products.Add(product);
        return Ok(new
        {
            Success = true,
            data = product,
        });
    }

    // PUT api/values/5

    [HttpPut("{id}")]

    public IActionResult Put(string id, [FromBody] ProductVM productEdit)
    {
        // Linq query object
        var product = Products.FirstOrDefault(p => p.ProductId == Guid.Parse(id));
        if (product == null)
        {
            return NotFound();
        }
        if (id != product.ProductId.ToString())
        {
            return BadRequest();
        }
        //Update
        product.ProductName = productEdit.ProductName;
        product.ProductPrice = productEdit.ProductPrice;

        return Ok(new
        {
            success = true,
            data = product
        });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        try
        {
            var product = Products.FirstOrDefault(p => p.ProductId == Guid.Parse(id));
            if (product == null)
            {
                return NotFound();
            }
            Products.Remove(product);
            return Ok("Delete success");
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }

}