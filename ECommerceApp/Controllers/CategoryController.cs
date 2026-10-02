using ECommerceApp.DTOs;
using ECommerceApp.DTOs.CategoryDTOs;
using ECommerceApp.Services.CategoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("CreateCategory")]
        public async Task<ActionResult<ApiResponse<CategoryResponseDTO>>> CreateCategory([FromBody] CategoryCreateDTO categoryDto)
        {
            var response = await _categoryService.CreateCategoryAsync(categoryDto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
        [HttpGet("GetCategoryById/{id}")]

        public async Task<ActionResult<ApiResponse<CategoryResponseDTO>>> GetCategoryById(int id)
        {
            var response = await _categoryService.GetCategoryByIdAsync(id);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
        [HttpPut("UpdateCategory")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateCategory([FromBody] CategoryUpdateDTO categoryDto)
        {
            var response = await _categoryService.UpdateCategoryAsync(categoryDto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategoryAsync(id);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<ApiResponse<List<CategoryResponseDTO>>>> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategoriesAsync();
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
    }
}
