using AutoMapper;
using ecommerce_project.Dto;
using ecommerce_project.Interface;
using ecommerce_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_project.Controllers
{

    /// <summary>
    /// A product controller
    /// </summary>
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProduct _productRepository;
        private readonly ICategory _categoryRepository;
        private readonly IMapper _mapper;
        public ProductController(IProduct productRepository, ICategory categoryRepo, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepo;
            _mapper = mapper;
        }

        /// <summaty>
        /// Get a list of products
        /// </summaty>
        /// <returns>An IActionResult </returns>
        /// <response code="200">The product has successfully returned</response>
        /// <response code="404">The porduct not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetProducts()
        {
           var products=  _productRepository.GetProducts();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(products);
        }

        /// <summaty>
        /// Get product by product id
        /// </summaty>
        /// <param name="productId">The id of the product to get </param>
        /// <returns>An IActionResult </returns>
        /// <response code="200">The product has successfully returned </response>
        /// <response code="404">The porduct not found</response>
        [HttpGet("productId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProduct(int productId)
        {
            if(!_productRepository.ProductExists(productId))
                return NotFound();

            var product = _productRepository.GetProductById(productId);
            return Ok(product);
        }

        /// <summaty>
        /// Create a new product
        /// </summaty>
        /// <param name="productCreated">The product object that wnat to inserted </param>
        /// <returns>An IActionResult </returns>
        /// <response code="200">The product has successfully created</response>
        /// <response code="400">failed to create the porduct</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateProduct([FromBody] CreateProductDTO productCreated)
        {
            if (productCreated == null)
                return BadRequest();

            var product = _productRepository.GetProducts()
                .Where(p => p.Name.Trim().ToUpper() == productCreated.Name.TrimEnd().ToUpper()).SingleOrDefault();

            var catagory = _categoryRepository.CategoryExists(productCreated.CategoryId);
            if (!catagory)
            {
                ModelState.AddModelError("", "The category ID not exists");
                return StatusCode(422, ModelState);
            }

            if (product != null)
            {
                ModelState.AddModelError("", "Product already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productCreated);

            if (!_productRepository.CreateProduct(productMap))
            {
                ModelState.AddModelError("", "Samething went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok();


        }

        /// <summaty>
        /// Update a product by Id
        /// </summaty>
        /// <param name="productID">The id of the product to update</param>
        /// <param name="updatedProduct">The updated object of a product</param>
        /// <returns>An IActionResult </returns>
        /// <response code="200">Updated the product seccessfully</response>
        /// <response code="400">failed to find the porduct</response>
        /// <response code="404">The product not found in the system</response>
        [HttpPut("{productID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
