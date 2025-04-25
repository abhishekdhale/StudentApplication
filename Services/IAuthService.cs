using BasicApplication.DTOs;
using BasicApplication.Models;
namespace BasicApplication.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> LoginAsysnc(LoginDTO loginDTO);
        string GenerateJwtToken(User user);
    }
}
