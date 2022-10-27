using PasswordManagerConsumer.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore;
using PasswordManagerConsumer.Dtos;
using System.Text.Json;
using System.Text;
using System.Reflection.Metadata;
using Blazored.LocalStorage;
using System.Windows.Markup;
using Manager.BL.DTOs;

namespace PasswordManagerConsumer.Services
{
    public class UserService : RouteView ,IUserService
    {
        private readonly HttpClient client;
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public ILocalStorageService localStore;
        public string token;
        public UserService(HttpClient client, ILocalStorageService localStore)
        {
            this.client = client;
            this.localStore = localStore;
        }
        public async Task<IEnumerable<Employee>> GetUsers()
        {
            var token =  await localStore.GetItemAsync<string>("token");
            try
            {
               

                var request = new HttpRequestMessage(HttpMethod.Get, "api/Manager");
           
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using var httpResponse = await client.SendAsync(request);



                if(httpResponse.StatusCode== System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new Exception("You are Not Authorized");
                }
               else if (!httpResponse.IsSuccessStatusCode)
                {
                    // set error message for display, log to console and return
                    var message =await httpResponse.Content.ReadAsStringAsync();
                    throw new Exception(message);

                }
                else
                {
                    // convert http response data to UsersResponse object
                    var response = await httpResponse.Content.ReadFromJsonAsync<IEnumerable<Employee>>();

                    return response;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task<string> CheckLogin(UserLoginDto user)
        {
            using var response = await client.PostAsJsonAsync("api/Manager/Login", user);
            if (!response.IsSuccessStatusCode)
            {
                // set error message for display, log to console and return

                Console.WriteLine($"There was an error! ");
                return null;
            }
            else
            {
                // convert response data to Article object
                var data = await response.Content.ReadFromJsonAsync<JsonElement>();
                return data.GetProperty("token").GetString();
            }
        }
        public async Task<string> Register(UserRegisterDto user)
        {
            var token = await localStore.GetItemAsync<string>("token");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Manager/Register");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
            using var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                
                Console.WriteLine($"There was an error! {response.ReasonPhrase}");
                var cont = await response.Content.ReadFromJsonAsync<UserReadDto>();
                if (cont.UserNameExists)
                {
                    return "User Name Exists";
                }
                return response.ReasonPhrase;
            }
            else
            {
                return "Ok";
            }
        }

        public async Task<Employee> GetUserById(string id)
        {
        //    System.Threading.Thread.Sleep(3000);

            Console.WriteLine(token);
            try
            {

                var type = await localStore.KeysAsync();
                var token = await localStore.GetItemAsync<string>("token");

                var request = new HttpRequestMessage(HttpMethod.Get, $"api/Manager/{id}");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using var httpResponse = await client.SendAsync(request);



                if (httpResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new Exception("You are Not Authorized");
                }
                else if (!httpResponse.IsSuccessStatusCode)
                {
                    // set error message for display, log to console and return
                    var message = await httpResponse.Content.ReadAsStringAsync();
                    throw new Exception(message);

                }
                else
                {
                    // convert http response data to UsersResponse object
                    var response = await httpResponse.Content.ReadFromJsonAsync<Employee>();

                    return response;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
           
        }

        public async Task<string> EditUser(Employee emp)
        {
            var token = await localStore.GetItemAsync<string>("token");
            Console.WriteLine(emp.UserName);
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/Manager/Edit/{emp.Id}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new StringContent(JsonSerializer.Serialize(emp), Encoding.UTF8, "application/json");
            Console.WriteLine(request.Content);
            using var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {

                Console.WriteLine($"There was an error! {response.ReasonPhrase}");
                return response.ReasonPhrase;
            }
            else
            {
                return "Ok";
            }
        }
        public async Task<string> DeleteById(string id)
        {
            var token = await localStore.GetItemAsync<string>("token");
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Manager/{id}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Console.WriteLine(request.Content);
            using var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {

                Console.WriteLine($"There was an error! {response.ReasonPhrase}");
                return response.ReasonPhrase;
            }
            else
            {
                return "Ok";
            }
        }
    }
}
