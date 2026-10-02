using ECommerceApp.DTOs;
using ECommerceApp.DTOs.CategoryDTOs;

namespace ECommerceApp.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<ApiResponse<CategoryResponseDTO>> CreateCategoryAsync(CategoryCreateDTO categoryDto);
        Task<ApiResponse<CategoryResponseDTO>> GetCategoryByIdAsync(int id);
        Task<ApiResponse<ConfirmationResponseDTO>> UpdateCategoryAsync(CategoryUpdateDTO categoryDto);
        Task<ApiResponse<ConfirmationResponseDTO>> DeleteCategoryAsync(int id);
        Task<ApiResponse<List<CategoryResponseDTO>>> GetAllCategoriesAsync();

    }
}
