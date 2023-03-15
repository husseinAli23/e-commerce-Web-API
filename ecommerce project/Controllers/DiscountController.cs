using AutoMapper;
using ecommerce_project.Interface;
using ecommerce_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_project.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountController : Controller
{
    private readonly IDiscount _discountRepository;
    private readonly IMapper _mapper;

    public DiscountController(IDiscount discount,IMapper mapper)
    {
        _discountRepository = discount;
        _mapper = mapper;
    }


    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Discount>))]
    public IActionResult GetDiscounts()
    {
        var products = _discountRepository.GetDiscounts();

        if (!ModelState.IsValid)
            return BadRequest();

        return Ok(products);
    }

    [HttpGet("discuntId")]
    [ProducesResponseType(200, Type = typeof(Discount))]
    public IActionResult GetDiscount(int discuntId)
    {
        if (!_discountRepository.DiscountExists(discuntId))
            return NotFound();

        var product = _discountRepository.GetDiscount(discuntId);
        return Ok(product);
    }


    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateDiscount([FromBody] Discount discountCreated)
    {
        if (discountCreated == null)
            return BadRequest();

        var discount = _discountRepository.GetDiscounts()
            .Where(d => d.DiscountPercentage == discountCreated.DiscountPercentage).FirstOrDefault();

        if (discount != null)
        {
            ModelState.AddModelError("", "This percentage of discount already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var discountMap = _mapper.Map<Discount>(discountCreated);

        if (!_discountRepository.CreateDiscount(discountCreated))
        {
            ModelState.AddModelError("", "Samething went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");


    }

}
