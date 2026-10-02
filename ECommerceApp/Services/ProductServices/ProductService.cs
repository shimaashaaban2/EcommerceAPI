using ECommerceApp.Data;
using ECommerceApp.DTOs;
using ECommerceApp.DTOs.ProductDTO;
using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<ProductResponseDTO>> CreateProductAsync(ProductCreateDTO productDto)
        {
            try
            {
                if(await _context.Products.AnyAsync(p=>p.Name== productDto.Name))
                {
                    return new ApiResponse<ProductResponseDTO>(400, "Product with the same name already exists.");
                }
                if(!await _context.Categories.AnyAsync(c=>c.Id== productDto.CategoryId))
                {
                    return new ApiResponse<ProductResponseDTO>(400, "Category does not exist.");
                }
                // Manual mapping from DTO to Model
                var product = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    StockQuantity = productDto.StockQuantity,
                    ImageUrl = productDto.ImageUrl,
                    DiscountPercentage = productDto.DiscountPercentage,
                    CategoryId = productDto.CategoryId,
                    IsAvailable = true
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                var productResponse = new ProductResponseDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    StockQuantity = product.StockQuantity,
                    ImageUrl = product.ImageUrl,
                    DiscountPercentage = product.DiscountPercentage,
                    CategoryId = product.CategoryId,
                    IsAvailable = product.IsAvailable
                };
                return new ApiResponse<ProductResponseDTO>(200,productResponse);
            }
            catch (Exception ex)
            {

                return new ApiResponse<ProductResponseDTO>(500, $"An unexcepted error occurred while processing your request, Error: {ex.Message}");
            }
        }
        public async Task<ApiResponse<ProductResponseDTO>> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                if (product == null)
                {
                    return new ApiResponse<ProductResponseDTO>(404, "Product not found.");
                }
                //Mapping to ProductResponseDTO
                var productResponse = new ProductResponseDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    StockQuantity = product.StockQuantity,
                    ImageUrl = product.ImageUrl,
                    DiscountPercentage = product.DiscountPercentage,
                    CategoryId = product.CategoryId,
                    IsAvailable = product.IsAvailable
                };

                return new ApiResponse<ProductResponseDTO>(200,productResponse);
            }
            catch (Exception ex)
            {

                return new ApiResponse<ProductResponseDTO>(500, $"An unexcepted error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateProductAsync(ProductUpdateDTO productDto)
        {
            try
            {
                var product = await _context.Products.FindAsync( productDto.Id);
                if (product == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "Product not found.");
                }
                if(await _context.Products.AnyAsync(p=>p.Name.ToLower()==productDto.Name.ToLower()&& p.Id != productDto.Id))
                {
                    return new ApiResponse<ConfirmationResponseDTO>(400, "Another Product with the same name already exists.");
                }

                if (!await _context.Categories.AnyAsync(c => c.Id == productDto.CategoryId))
                {
                    return new ApiResponse<ConfirmationResponseDTO>(400, "Category does not exist.");
                }
                // Update product properties manually
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;
                product.StockQuantity = productDto.StockQuantity;
                product.ImageUrl = productDto.ImageUrl;
                product.DiscountPercentage = productDto.DiscountPercentage;
                product.CategoryId = productDto.CategoryId;
                await _context.SaveChangesAsync();
                var confirmationResponse = new ConfirmationResponseDTO
                {
                    Message = $"Product with Id {product.Id} updated successfully."
                };
                return new ApiResponse<ConfirmationResponseDTO>(200, confirmationResponse);
            }
            catch (Exception ex)
            {

                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexcepted error occurred while processing your request, Error: {ex.Message}");
            }
        }
        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                if(product == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "Product not found.");
                }
                product.IsAvailable = false;
                await _context.SaveChangesAsync();
                var confirmationResponse = new ConfirmationResponseDTO
                {
                    Message = $"Product with Id {product.Id} deleted successfully."
                };
                return new ApiResponse<ConfirmationResponseDTO>(200, confirmationResponse);
            }
            catch (Exception ex)
            {

                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexpceted error occurred while processing your request, Error:{ex.Message}");

            }
        }
        public async Task<ApiResponse<List<ProductResponseDTO>>> GetAllProductsAsync()
        {
            try
            {
                var products = await _context.Products
                    .AsNoTracking()
                    .ToListAsync();
                var productList = products.Select(p => new ProductResponseDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    ImageUrl = p.ImageUrl,
                    DiscountPercentage = p.DiscountPercentage,
                    CategoryId = p.CategoryId,
                    IsAvailable = p.IsAvailable
                }).ToList();
                return new ApiResponse<List<ProductResponseDTO>>(200, productList);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<List<ProductResponseDTO>>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }
        public async Task<ApiResponse<List<ProductResponseDTO>>> GetAllProductsByCategoryAsync(int categoryId)
        {
            try
            {
                // Retrieve products associated with the specified category
                var products = await _context.Products
                    .AsNoTracking()
                    .Include(p => p.Category)
                    .Where(p => p.CategoryId == categoryId && p.IsAvailable)
                    .ToListAsync();
                if (products == null || products.Count == 0)
                {
                    return new ApiResponse<List<ProductResponseDTO>>(404, "Products not found.");
                }
                var productList = products.Select(p => new ProductResponseDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    ImageUrl = p.ImageUrl,
                    DiscountPercentage = p.DiscountPercentage,
                    CategoryId = p.CategoryId,
                    IsAvailable = p.IsAvailable
                }).ToList();
                return new ApiResponse<List<ProductResponseDTO>>(200, productList);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<List<ProductResponseDTO>>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateProductStatusAsync(ProductStatusUpdateDTO productStatusUpdateDTO)
        {
            try
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == productStatusUpdateDTO.ProductId);
                if (product == null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(404, "Product not found.");
                }
                product.IsAvailable = productStatusUpdateDTO.IsAvailable;
                await _context.SaveChangesAsync();
                // Prepare confirmation message
                var confirmationMessage = new ConfirmationResponseDTO
                {
                    Message = $"Product with Id {productStatusUpdateDTO.ProductId} Status Updated successfully."
                };
                return new ApiResponse<ConfirmationResponseDTO>(200, confirmationMessage);
            }
            catch (Exception ex)
            {
                // Log the exception
                return new ApiResponse<ConfirmationResponseDTO>(500, $"An unexpected error occurred while processing your request, Error: {ex.Message}");
            }
        }


    }
}
