using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using PasswordManagerConsumer.Models;
using PasswordManagerConsumer.Services;
using System.Reflection.Metadata;

namespace PasswordManagerConsumer.Pages
{
    public class EditBase : ComponentBase
    {
        [Inject]
        public IUserService userService { get; set; }
        public Employee employee { get; set; } = new Employee();
        [Parameter]
        public string id { get; set; }
        public string ErrorMessage { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
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
        protected override async Task OnInitializedAsync()
        {
            IsAuthorized = await isAdmin();
            try
            {
                employee = await userService.GetUserById(id);
            }
            catch (Exception ex)
            {
                employee = null;
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
