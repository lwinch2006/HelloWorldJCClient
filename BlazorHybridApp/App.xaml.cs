namespace BlazorHybridApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new MainPage();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var window = new CustomWindow(MainPage!)
		{
			Title = BlazorHubridAppConstants.WindowTitle,
			MinimumHeight = 1000.0,
			MaximumHeight = 1000.0,
			MinimumWidth = 500.0,
			MaximumWidth = 500.0
		};

		return window;
	}
}