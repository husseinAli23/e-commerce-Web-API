using AutoMapper;
using ecommerce_project.Dto;
using ecommerce_project.Interface;
using ecommerce_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_project.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class ProductController : Controller
    {
        private readonly IProduct _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProduct Iproduct, IMapper Imapper)
        {
            _productRepository = Iproduct;
            _mapper = Imapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        {
           var products=  _productRepository.GetProducts("asd");

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(products);
        }

        [HttpGet("productId")]
        [ProducesResponseType(200, Type = typeof(Product))]
        public IActionResult GetProduct(int productId)
        {
            if(!_productRepository.ProductExists(productId))
                return NotFound();

            var product = _productRepository.GetProductById(productId);
            return Ok(product);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromBody] Product productCreated)
        {
            if (productCreated == null)
                return BadRequest();

            var product = _productRepository.GetProducts()
                .Where(p => p.Name.Trim().ToUpper() == productCreated.Name.TrimEnd().ToUpper()).SingleOrDefault();

            if (product != null)
            {
                ModelState.AddModelError("", "Product already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

          //  var productMap = _mapper.Map<Product>(productCreated);

            if (!_productRepository.CreateProduct(productCreated))
            {
                ModelState.AddModelError("", "Samething went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");


        }

        [HttpPut("{productID}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateProduct(int productID, [FromBody] ProductDto updatedProduct)
        {

            if (updatedProduct == null)
                return BadRequest(ModelState);

            if (productID != updatedProduct.Id)
                return BadRequest(ModelState);

            if (!_productRepository.ProductExists(productID))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var categoryMap = _mapper.Map<Product>(updatedProduct);

            if (!_productRepository.UpdateProduct(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong when updating category");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated seccessfully");
        }


    }
}
