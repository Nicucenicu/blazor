using BlazorShop.Shared;
namespace BlazorShop.Client.Services
{  
          public interface IAuthService
          {
               Task<ServiceResponse<int>> Register(UserRegisterDto request);
               Task<ServiceResponse<string>> Login(UserLoginDto request);
               Task<ServiceResponse<bool>> ChangePassword(ResetPasswordDto request);
               Task<bool> IsUserAuthenticated();
          }
     

}
