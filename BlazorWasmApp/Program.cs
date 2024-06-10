using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWasmApp;
using BusinessLogic;
using Fluxor;
using Infrastructure;
using JCClientCore;
using UiShared.Models.Blazor.Redux.Stores;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
	.AddSingleton<IJCClient, JCClient>()
	.AddScoped<IUserRepository, DummyUserRepository>()
	.AddScoped<IUserService, UserService>()
	.AddFluxor(options =>
	{
		options.ScanAssemblies(typeof(AppStore).Assembly);
	})
	.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();