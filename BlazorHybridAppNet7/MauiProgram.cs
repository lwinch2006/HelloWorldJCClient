using Microsoft.Extensions.Logging;
using BusinessLogic;
using Fluxor;
using Infrastructure;
using JCClientCore;
using UiShared.Models.Blazor.Redux.Stores;

namespace BlazorHybridAppNet7;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

		builder.Services
			.AddSingleton<IJCClient, JCClient>()
			.AddScoped<IUserRepository, JCUserRepository>()
			.AddScoped<IUserService, UserService>()
			.AddFluxor(options =>
			{
				options.WithLifetime(StoreLifetime.Singleton);
				options.ScanAssemblies(typeof(AppStore).Assembly);
			})
			.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost") })
			.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}