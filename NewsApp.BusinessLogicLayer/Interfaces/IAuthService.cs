using NewsApp.ApplicationLayer.Dtos.AuthDtos;

namespace NewsApp.ApplicationLayer.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    }
}
