using Cyper.Models.Identity;

namespace Cyper.Services.Interface
{
    public interface IAuthRepository
    {
        Task<AuthModel> RegisterAsync(RegisterModel model, string RoleName);

        Task<AuthModel> LoginAsync(LoginModel model);

        Task<string> ConfirmEmailAsync(string Email, string Token);

        Task<string> SendResetPasswordAsync(string Email);

        Task<string> ConfirmResetPasswordAsync(string Code, string Email);

        Task<string> ResetPasswordAsync(string Email, string Password);
    }
}
