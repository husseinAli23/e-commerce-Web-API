﻿using AutoMapper;
using ecommerce_project.Dto;
using ecommerce_project.Interface;
using ecommerce_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_project.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : Controller
{
    private readonly ICategory _catagoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategory Icatagory, IMapper Imapper)
    {
        _catagoryRepository = Icatagory;
        _mapper = Imapper;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product_category>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetCategory()
    {
        var Categories = _catagoryRepository.GetCategories();

        if (!ModelState.IsValid)
            return BadRequest();

        return Ok(Categories);
    }

    [HttpGet("categoryId")]
    [ProducesResponseType(200, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetCategory(int categoryId)
    {
        if (!_catagoryRepository.CategoryExists(categoryId))
            return NotFound();

        var product = _catagoryRepository.GetCategory(categoryId);
        return Ok(product);
    }


    [HttpPost]
    [Route("createCategory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateCategory([FromBody] Product_category categoryCreated)
    {
        if (categoryCreated == null)
            return BadRequest();

        var categoty = _catagoryRepository.GetCategories()
            .Where(c => c.Name.Trim().ToUpper() == categoryCreated.Name.Trim().ToUpper()).FirstOrDefault();

        if (categoty != null)
        {
            ModelState.AddModelError("", "Category already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var categoryMap = _mapper.Map<Product_category>(categoryCreated);

        if (!_catagoryRepository.CreateCatagory(categoryCreated))
        {
            ModelState.AddModelError("", "Samething went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok();


    }

  
    [HttpPut("{categoryID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateCategory(int categoryID, [FromBody] CategoryDto updatedCategory)
    {

        if (updatedCategory == null)
            return BadRequest(ModelState);

        if (categoryID != updatedCategory.Id)
            return BadRequest(ModelState);

        if (!_catagoryRepository.CategoryExists(categoryID))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var categoryMap = _mapper.Map<Product_category>(updatedCategory);

        if (!_catagoryRepository.UpdateCatagory(categoryMap))
        {
            ModelState.AddModelError("", "Something went wrong when updating category");
            return StatusCode(500, ModelState);
        }

        return Ok();
    }
}
