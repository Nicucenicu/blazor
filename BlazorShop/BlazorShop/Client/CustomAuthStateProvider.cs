﻿using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Primitives;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;


namespace BlazorShop.Client
{
     public class CustomAuthStateProvider : AuthenticationStateProvider
     {
          private readonly IJSRuntime _JSRuntime;

          public CustomAuthStateProvider(IJSRuntime JSRuntime)
          {
               _JSRuntime = JSRuntime;
          }
          public override async Task<AuthenticationState> GetAuthenticationStateAsync()
          {
               var json = await _JSRuntime.InvokeAsync<string>("localStorage.getItem","token");
               string token = JsonSerializer.Deserialize<String>(json);

               var identity = new ClaimsIdentity();
               if (!string.IsNullOrEmpty(token))
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

               var user = new ClaimsPrincipal(identity);
               var state = new AuthenticationState(user);

               NotifyAuthenticationStateChanged(Task.FromResult(state));

               return state;
          }

          public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
          {
               var payload = jwt.Split('.')[1];
               var jsonBytes = ParseBase64WithoutPadding(payload);
               var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
               return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
          }

          private static byte[] ParseBase64WithoutPadding(string base64)
          {
               switch (base64.Length % 4)
               {
                    case 2: base64 += "=="; break;
                    case 3: base64 += "="; break;
               }
               return Convert.FromBase64String(base64);
          }
     }
}
