using ECommerceApp.DTOs;
using ECommerceApp.DTOs.ShoppingCartDTOs;

namespace ECommerceApp.Services.ShoppingCartServices
{
    public interface IShoppingCartService
    {
        Task<ApiResponse<CartResponseDTO>> GetCartByCustomerIdAsync(int customerId);
        Task<ApiResponse<CartResponseDTO>> AddToCartAsync(AddToCartDTO addToCartDTO);
        Task<ApiResponse<CartResponseDTO>> UpdateCartItemAsync(UpdateCartItemDTO updateCartItemDTO);
        Task<ApiResponse<CartResponseDTO>> RemoveCartItemAsync(RemoveCartItemDTO removeCartItemDTO);
        Task<ApiResponse<ConfirmationResponseDTO>> ClearCartAsync(int customerId);



    }
}
