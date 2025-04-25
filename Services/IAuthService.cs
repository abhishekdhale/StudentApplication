using StudentManagement.DTOs;

namespace StudentManagement.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
} 