using Fluxor;
using Microsoft.AspNetCore.Components;
using UiShared.Models.Blazor.Redux.Stores;

namespace BlazorHybridApp.Pages;

public partial class Update
{
	[Inject] private IState<AppStore> State { get; set; } = default!;
	
	[Inject] private Fluxor.IDispatcher Dispatcher { get; set; } = default!;

	private AppStore AppStore => State.Value;

	protected override void OnInitialized()
	{
		base.OnInitialized();
	}
}