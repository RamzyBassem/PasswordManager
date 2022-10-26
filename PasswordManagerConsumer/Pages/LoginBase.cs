using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using PasswordManagerConsumer.Dtos;
using PasswordManagerConsumer.Services;
using System;
using System.Reflection.Metadata;

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
                await localStore.SetItemAsync("token", token);
                // NavigationManager.NavigateTo("/");
                NavigationManager.NavigateTo("/", forceLoad: true);
            }
        }
    }
}
