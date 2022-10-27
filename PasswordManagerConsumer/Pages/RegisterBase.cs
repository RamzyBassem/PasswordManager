using Microsoft.AspNetCore.Components;
using PasswordManagerConsumer.Dtos;
using PasswordManagerConsumer.Services;

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

    }
}
