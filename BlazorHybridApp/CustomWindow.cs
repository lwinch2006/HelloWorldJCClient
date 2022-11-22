using Microsoft.Maui.Handlers;

namespace BlazorHybridApp;

public class CustomWindow : Window
{
	public CustomWindow() : base()
	{}
	
	public CustomWindow(Page page) : base(page)
	{}

	protected override void OnHandlerChanged()
	{
		base.OnHandlerChanged();

		if (Handler is WindowHandler windowHandler)
		{
			#if WINDOWS
                // If you don't want to use our custom title bar
                // handler.PlatformView.ExtendsContentIntoTitleBar = false;
			#elif MACCATALYST
				windowHandler.PlatformView.WindowScene!.Title = BlazorHubridAppConstants.WindowTitle;
			#endif			
		}
	}
}