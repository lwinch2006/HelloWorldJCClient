namespace BlazorHybridAppNet7;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new MainPage();
	}
	
	protected override Window CreateWindow(IActivationState? activationState)
	{
		var window = base.CreateWindow(activationState);
		
		window.Title = BlazorHubridAppConstants.WindowTitle;
		window.MinimumHeight = 1050.0;
		window.MaximumHeight = 1050.0;
		window.MinimumWidth = 500.0;
		window.MaximumWidth = 500.0;
		
		return window;
	}
}