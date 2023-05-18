using BlazorShop.Server.Migrations;
using BlazorShop.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BlazorShop.Server.services
{
    public interface ILoginService
    {
          Task<UserDb> Register(UserRegisterDto request);
          Task<IActionResult> Login(UserLoginDto request);
          Task<IActionResult> Verify(string token);
          Task<IActionResult> ForgotPassword(string email);
          Task<IActionResult> ResettPassword(ResetPasswordDto request);


    }
}
