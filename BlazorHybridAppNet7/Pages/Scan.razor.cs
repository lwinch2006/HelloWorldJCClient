using Fluxor;
using Microsoft.AspNetCore.Components;
using UiShared.Models.Blazor.Redux.Actions.Card;
using UiShared.Models.Blazor.Redux.Actions.UserRead;
using UiShared.Models.Blazor.Redux.Stores;

namespace BlazorHybridAppNet7.Pages;

public partial class Scan
{
	[Inject] private IState<AppStore> State { get; set; } = default!;
	[Inject] private Fluxor.IDispatcher Dispatcher { get; set; } = default!;
	private AppStore AppStore => State.Value;
	
	protected override void OnInitialized()
	{
		Dispatcher.Dispatch(new InitMonitorAction(Refresh, ClearWindow));
		base.OnInitialized();
	}
	
	private void ClearWindow()
	{
		Dispatcher.Dispatch(new ClearAction());
	}
	
	private void Refresh()
	{
		Dispatcher.Dispatch(new ReadFromCardAction());
	}
}