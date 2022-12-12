using Fluxor;
using Microsoft.AspNetCore.Components;
using UiShared.Models.Blazor.Redux.Actions.UserRead;
using UiShared.Models.Blazor.Redux.Stores;

namespace BlazorWasmApp.Pages;

public partial class Scan
{
	[Inject] private IState<AppStore> State { get; set; } = default!;
	[Inject] private IDispatcher Dispatcher { get; set; } = default!;
	private AppStore AppStore => State.Value;

	protected override void OnInitialized()
	{
		Dispatcher.Dispatch(new ClearAction());
		base.OnInitialized();
	}

	private void Refresh()
	{
		Dispatcher.Dispatch(new ReadDummyAction());
	}
}