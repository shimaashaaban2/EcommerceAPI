using ECommerceApp.DTOs;
using ECommerceApp.DTOs.CancellationDTOs;

namespace ECommerceApp.Services.CancellationServices
{
    public interface ICancellationService
    {
        Task<ApiResponse<CancellationResponseDTO>> RequestCancellationAsync(CancellationRequestDTO cancellationRequest);
        Task<ApiResponse<CancellationResponseDTO>> GetCancellationByIdAsync(int id);
        Task<ApiResponse<ConfirmationResponseDTO>> UpdateCancellationStatusAsync(CancellationStatusUpdateDTO statusUpdate);
        Task<ApiResponse<List<CancellationResponseDTO>>> GetAllCancellationsAsync();
    }
}
