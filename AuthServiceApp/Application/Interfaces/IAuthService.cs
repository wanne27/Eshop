using AuthServiceApp.Application.DTOs;

namespace AuthServiceApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<TokenResponseDto> LoginAsync(LoginModel model);
    }
}
