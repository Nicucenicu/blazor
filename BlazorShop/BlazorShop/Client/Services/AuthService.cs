﻿/*using BlazorShop.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace BlazorShop.Client.Services
{
    public class AuthService: IAuthService
    {
        *//*  private readonly HttpClient _http;
          private readonly AuthenticationStateProvider _authStateProvider;

          public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
          {
               _http = http;
               _authStateProvider = authStateProvider;
          }

          public async Task<ServiceResponse<bool>> ChangePassword(ResetPasswordDto request)
          {
               var result = await _http.PostAsJsonAsync("api/auth/change-password", request.Password);
               return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
          }

          public async Task<bool> IsUserAuthenticated()
          {
               return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
          }

          public async Task<ServiceResponse<string>> Login(UserLoginDto request)
          {
               var result = await _http.PostAsJsonAsync("api/auth/login", request);
               return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
          }

          public async Task<ServiceResponse<int>> Register(UserRegisterDto request)
          {
               var result = await _http.PostAsJsonAsync("api/auth/register", request);
               return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
          }*//*
     }
}
*/