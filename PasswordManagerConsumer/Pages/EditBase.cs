using Microsoft.AspNetCore.Components;
using PasswordManagerConsumer.Models;
using PasswordManagerConsumer.Services;

namespace PasswordManagerConsumer.Pages
{
    public class EditBase : ComponentBase
    {
        [Inject]
        public IUserService userService { get; set; }
        public Employee employee { get; set; }
        [Parameter]
        public string id { get; set; }
        public string ErrorMessage { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                employee = await userService.GetUserById(id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }
        public async Task EditUser()
        {
           var result= await userService.EditUser(employee);
          
            if (result == "Ok")
            {
                NavigationManager.NavigateTo("/fetchdata", forceLoad: true);

            }
           
        }
        
        
    }
}
