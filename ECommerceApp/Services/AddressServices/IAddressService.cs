using ECommerceApp.DTOs;
using ECommerceApp.DTOs.AddressesDTOs;

namespace ECommerceApp.Services.AddressServices
{
    public interface IAddressService
    {
        Task<ApiResponse<AddressResponseDTO>> CreateAddressAsync(AddressCreateDTO addressDto);
        Task<ApiResponse<AddressResponseDTO>> GetAddressByIdAsync(int id);
        Task<ApiResponse<ConfirmationResponseDTO>> UpdateAddressAsync(AddressUpdateDTO addressDto);
        Task<ApiResponse<ConfirmationResponseDTO>> DeleteAddressAsync(AddressDeleteDTO addressDeleteDTO);
        Task<ApiResponse<List<AddressResponseDTO>>> GetAddressesByCustomerAsync(int customerId);
    }
}
