using ECommerceApp.DTOs;
using ECommerceApp.DTOs.ProductDTO;
using ECommerceApp.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("CreatteProduct")]
        public async Task<ActionResult<ApiResponse<ProductResponseDTO>>> CreateProduct([FromBody] ProductCreateDTO productCreateDTO)
        {
            try
            {
                var response = await _productService.CreateProductAsync(productCreateDTO);
                if (response.StatusCode != 200)
                {
                    return StatusCode(response.StatusCode, response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {

               return BadRequest($"{ex.Message}");
            }
           
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateProduct([FromBody] ProductUpdateDTO productUpdateDTO) 
        {
            try
            {
                var response = await _productService.UpdateProductAsync(productUpdateDTO);

                if(response.StatusCode != 200)
                {
                    return StatusCode(response.StatusCode, response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {

                return  BadRequest($" {ex.Message}");
                    
            }
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<ApiResponse<ProductResponseDTO>>> GetProductById(int id)
        {
            try
            {
                var response = await _productService.GetProductByIdAsync(id);
                if (response.StatusCode != 200)
                {
                    return StatusCode(response.StatusCode, response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // Deletes a product by ID.
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteProduct(int id)
        {
            try
            {
                var response = await _productService.DeleteProductAsync(id);
                if (response.StatusCode != 200)
                {
                    return StatusCode(response.StatusCode, response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest($"Product {ex.Message}");
            }
           
        }

        // Retrieves all products.
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<ApiResponse<List<ProductResponseDTO>>>> GetAllProducts()
        {
            var response = await _productService.GetAllProductsAsync();
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }


        // Retrieves all products by category.
        [HttpGet("GetAllProductsByCategory/{categoryId}")]
        public async Task<ActionResult<ApiResponse<List<ProductResponseDTO>>>> GetAllProductsByCategory(int categoryId)
        {
            try
            {
                var response = await _productService.GetAllProductsByCategoryAsync(categoryId);
                if (response.StatusCode != 200)
                {
                    return StatusCode(response.StatusCode, response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest($"{ex.Message}");
            }

        }

        // Update Product Status
        [HttpPut("UpdateProductStatus")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateProductStatus(ProductStatusUpdateDTO productStatusUpdateDTO)
        {
            try
            {
                var response = await _productService.UpdateProductStatusAsync(productStatusUpdateDTO);
                if (response.StatusCode != 200)
                {
                    return StatusCode(response.StatusCode, response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest($"{ex.Message}");
            }
           
        }
    }
}
 