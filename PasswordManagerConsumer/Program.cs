using Blazored.LocalStorage;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PasswordManagerConsumer;
using PasswordManagerConsumer.Dtos;
using PasswordManagerConsumer.Services;
using PasswordManagerConsumer.Validations;
using System;
using System.IdentityModel.Tokens.Jwt;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7235/") });
builder.Services.AddScoped<IUserService, UserService>();

await builder.Build().RunAsync();
_ = new JwtHeader();
_ = new JwtPayload();