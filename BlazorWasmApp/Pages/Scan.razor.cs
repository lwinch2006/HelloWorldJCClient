using Fluxor;
using Microsoft.AspNetCore.Components;
using UiShared.Models.Blazor.Redux.Actions;
using UiShared.Models.Blazor.Redux.Stores;

namespace BlazorWasmApp.Pages;

public partial class Scan
{
	[Inject] private HttpClient Http { get; init; } = default!;

	[Inject] private IState<AppStore> State { get; set; } = default!;
	
	[Inject] private IDispatcher Dispatcher { get; set; } = default!;

	private AppStore AppStore => State.Value;

	private async Task Refresh()
	{
		await Task.Delay(10);
		
		Dispatcher.Dispatch(new ReadDummyUserFromCardAction());
	}
}