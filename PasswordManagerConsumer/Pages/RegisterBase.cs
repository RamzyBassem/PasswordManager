using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using PasswordManagerConsumer.Dtos;
using PasswordManagerConsumer.Services;
using System.Reflection.Metadata;

namespace PasswordManagerConsumer.Pages
{
    public class RegisterBase : ComponentBase
    {
        public UserRegisterDto userRegisterDto { get; set; } = new UserRegisterDto();
        [Inject]
        public IUserService UserService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string Message { get; set; } = "";
        [Inject]
        public ILocalStorageService localStore { get; set; }
        public bool IsAuthorized { get; set; }
        public async Task<bool> isAdmin()
        {
            var role = await localStore.GetItemAsync<string>("role");
            if (role == "Admin")
                return true;
            else
                return false;
        }
        public async void RegisterUser()
        {
            string result = await UserService.Register(userRegisterDto);
           if(result == "User Name Exists")
            {
                Message = result;
            }
           if(result == "Ok")
            {
                NavigationManager.NavigateTo("/fetchdata", forceLoad: true);
            }

        }
        protected override async Task OnInitializedAsync()
        {
          IsAuthorized= await isAdmin();
        }


    }
}
