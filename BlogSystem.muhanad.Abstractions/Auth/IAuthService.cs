using BlogSystem.muhanad.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.muhanad.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<UserResponse> LoginAsync(LoginRequest request);
        Task<UserResponse> RegisterAsync(RegisterRequest request);
        Task ForgetPasswordSendEmail(ForgetPasswordDto dto);
        Task ResetPasswordAsync(ResetPasswordDto dto);
     }
}
