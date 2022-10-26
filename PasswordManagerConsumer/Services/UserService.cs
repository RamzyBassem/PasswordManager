using PasswordManagerConsumer.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore;
using PasswordManagerConsumer.Dtos;
using System.Text.Json;
using System.Text;

namespace PasswordManagerConsumer.Services
{
    public class UserService : RouteView ,IUserService
    {
        private readonly HttpClient client;
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public UserService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<IEnumerable<Employee>> GetUsers()
        {
            
            try
            {
                //  client.DefaultRequestHeaders.Authorization
                //     = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImVkZDZlOTM4LTk4MDItNDhkOS04ZTgyLTliOGZlYjU5N2E3NCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNjY2ODEyMzg3fQ.uLxcLCF84oG0uXS5qMgoW8352IwUy-hWz_ul9-82Nwg");
                // var emp = await this.client.GetFromJsonAsync<IEnumerable<Employee>>("api/Manager");

                var request = new HttpRequestMessage(HttpMethod.Get, "api/Manager");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImVkZDZlOTM4LTk4MDItNDhkOS04ZTgyLTliOGZlYjU5N2E3NCIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNjY2ODEyMzg3fQ.uLxcLCF84oG0uXS5qMgoW8352IwUy-hWz_ul9-82Nwg");
                using var httpResponse = await client.SendAsync(request);



                //  using var httpResponse = await client.GetAsync("api/Manager");
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
    }
}
