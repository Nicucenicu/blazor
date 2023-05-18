using BlazorShop.Server.Migrations;
using BlazorShop.Server.services;
using BlazorShop.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BlazorShop.Server.Controllers
{
     [Route("api/users")]
     [ApiController]
     public class UserController : ControllerBase
     {
          private readonly ILoginService _loginServices;

          public UserController(ILoginService loginServices)
          {
               _loginServices = loginServices;
          }


          [HttpPost("/register")]
          public async Task<UserDb> Register(UserRegisterDto request)
          {
               return await _loginServices.Register(request);
          }

          [HttpPost("/login")]
          public async Task<IActionResult> Login(UserLoginDto request)
          {
               return await _loginServices.Login(request);
          }

          [HttpPost("verify")]
          public async Task<IActionResult> Verify(string token)
          {
               return await _loginServices.Verify(token);
          }

          [HttpPost("forgot-password")]
          public async Task<IActionResult> ForgotPassword(string email)
          {
               return await _loginServices.ForgotPassword(email);
          }

          [HttpPost("reset-password")]
          public async Task<IActionResult> ResettPassword(ResetPasswordDto request)
          {
               return await _loginServices.ResettPassword(request);
          }

     }
}
