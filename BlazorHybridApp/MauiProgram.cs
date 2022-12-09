using Fluxor;
using Microsoft.Extensions.Logging;
using UiShared.Models.Blazor.Redux.Stores;

namespace BlazorHybridApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

		builder.Services
			.AddFluxor(options =>
			{
				options.ScanAssemblies(typeof(AppStore).Assembly);
			})
			.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
		return builder.Build();
	}
}