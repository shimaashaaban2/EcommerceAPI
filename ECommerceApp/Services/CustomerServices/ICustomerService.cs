using ECommerceApp.DTOs;
using ECommerceApp.DTOs.CustomerDTOs;

namespace ECommerceApp.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<ApiResponse<CustomerResponseDTO>> RegisterCustomerAsync(CustomerRegistrationDTO customerDto);
        Task<ApiResponse<LoginResponseDTO>> LoginAsync(LoginDTO loginDto);
        Task<ApiResponse<CustomerResponseDTO>> GetCustomerByIdAsync(int id);
        Task<ApiResponse<ConfirmationResponseDTO>> UpdateCustomerAsync(CustomerUpdateDTO customerDto);
        Task<ApiResponse<ConfirmationResponseDTO>> DeleteCustomerAsync(int id);
        Task<ApiResponse<ConfirmationResponseDTO>> ChangePasswordAsync(ChangePasswordDTO changePasswordDto);
    }
}
