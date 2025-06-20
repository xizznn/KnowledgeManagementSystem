using KnowledgeManagementSystem.Core.DTOs;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterUserDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginUserDto loginDto);
        Task<AuthResponseDto> RefreshTokenAsync(string token, string refreshToken);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}