using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using PasswordManagerConsumer.Dtos;
using PasswordManagerConsumer.Services;
using System;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;

namespace PasswordManagerConsumer.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public ILocalStorageService localStore { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public UserLoginDto userLoginDto { get; set; } = new UserLoginDto();
        public string ErrorMessage { get; set; } = null!;
        public async void HandleLogin()
        {
          var token = await  UserService.CheckLogin(userLoginDto);
            if (token == null)
            {
                ErrorMessage = "Invalid UserName Or Password";
                Console.WriteLine("Error");
            }
            else
            {
                
                Console.WriteLine($"Signed in with token = {token}");
                var jwtHandler = new JwtSecurityTokenHandler();

                var jwtToken = jwtHandler.ReadJwtToken(token);
                Console.WriteLine(jwtToken);

                var tokenS = jwtToken as JwtSecurityToken;

                var jti = tokenS.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
                await localStore.SetItemAsync("token", token);
                await localStore.SetItemAsync("role", jti);
                NavigationManager.NavigateTo("/", forceLoad: true);
            }
        }
    }
}
