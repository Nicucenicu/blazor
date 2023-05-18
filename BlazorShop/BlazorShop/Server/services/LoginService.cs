using BlazorShop.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BlazorShop.Server.services
{
     public class LoginService : ControllerBase, ILoginService
     {
          private readonly AppDbContext _db;

          public LoginService(AppDbContext db)
          {
               _db = db;
          }


          public async Task<UserDb> Register(UserRegisterDto request)
          {
               if (_db.User.Any(u => u.Email == request.Email))
               {
                    //return BadRequest("User already exists.");
               }

               CreatePasswordHash(request.Password,
                    out byte[] passwordHash,
                    out byte[] passwordSalt);

               var user = new UserDb
               {
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    VerifiedAt=DateTime.Now,
                    VerificationToken = CreateRandomToken()
               };

               _db.User.Add(user);
               await _db.SaveChangesAsync();

               return user;
          }
          
          public async Task<IActionResult> Login(UserLoginDto request)
          {
               var user = await _db.User.FirstOrDefaultAsync(u => u.Email == request.Email);
               if (user == null)
               {
                    return BadRequest("User not found.");
               }

               if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
               {
                    return BadRequest("Password is incorrect.");
               }

               if (user.VerifiedAt == null)
               {
                    return BadRequest("Not verified!");
               }

               return Ok($"Welcome back, {user.Email}! :)");
          }

          public async Task<IActionResult> ForgotPassword(string email)
          {
               var user = await _db.User.FirstOrDefaultAsync(u => u.Email == email);
               if (user == null)
               {
                    return BadRequest("User not found.");
               }

               user.PasswordResetToken = CreateRandomToken();
               user.ResetTokenExpires = DateTime.Now.AddDays(1);
               await _db.SaveChangesAsync();

               return Ok("You may now reset your password.");
          }

          public async Task<IActionResult> ResettPassword(ResetPasswordDto request)
          {
               var user = await _db.User.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
               if (user == null || user.ResetTokenExpires < DateTime.Now)
               {
                    return BadRequest("Invalid Token.");
               }

               CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

               user.PasswordHash = passwordHash;
               user.PasswordSalt = passwordSalt;
               user.PasswordResetToken = null;
               user.ResetTokenExpires = null;

               await _db.SaveChangesAsync();

               return Ok("Password successfully reset.");
          }


          public async Task<IActionResult> Verify(string token)
          {
               var user = await _db.User.FirstOrDefaultAsync(u => u.VerificationToken == token);
               if (user == null)
               {
                    return BadRequest("Invalid token.");
               }

               user.VerifiedAt = DateTime.Now;
               await _db.SaveChangesAsync();

               return Ok("User verified! :)");
          }

          private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
          {
               using (var hmac = new HMACSHA512())
               {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac
                        .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
               }
          }

          private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
          {
               using (var hmac = new HMACSHA512(passwordSalt))
               {
                    var computedHash = hmac
                        .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    return computedHash.SequenceEqual(passwordHash);
               }
          }

          private string CreateRandomToken()
          {
               return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
          }
     }
}


