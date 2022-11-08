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
		var window = base.CreateWindow(activationState);
		window.Title = "Test";
		window.Height = 300.0;
		window.Width = 300.0;
		return window;
	}
}