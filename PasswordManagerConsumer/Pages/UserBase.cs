using Microsoft.AspNetCore.Components;
using PasswordManagerConsumer.Models;
using PasswordManagerConsumer.Services;

namespace PasswordManagerConsumer.Pages
{
    public class UserBase : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }
        public IEnumerable<Employee> employees { get; set; }
        public string ErrorMessage { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                employees = await UserService.GetUsers();
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        public async Task DeleteUser(string id)
        {
            var result =await UserService.DeleteById(id);
            if (result == "Ok")
            {
                NavigationManager.NavigateTo("/fetchdata", true);

            }

        }


    }
}
