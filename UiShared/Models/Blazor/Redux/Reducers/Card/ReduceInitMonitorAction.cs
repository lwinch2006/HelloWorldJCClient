using Fluxor;
using JCClientCore;
using UiShared.Models.Blazor.Redux.Actions.Card;
using UiShared.Models.Blazor.Redux.Stores;

namespace UiShared.Models.Blazor.Redux.Reducers.Card;

public class InitMonitorActionReducer
{
	private readonly IJCClient _jcClient;
	
	public InitMonitorActionReducer(IJCClient jcClient)
	{
		_jcClient = jcClient;
	}
	
	[ReducerMethod]
	public AppStore ReduceInitMonitorAction(AppStore appStore, InitMonitorAction action)
	{
		string? errorMessage = null;
		
		try
		{
			_jcClient.StartCardMonitor(action.CardInserted, action.CardRemoved);
		}
		catch (Exception ex)
		{
			errorMessage = "JCClient initialization error";
			errorMessage = ex.ToString();
		}
		
		return appStore with { ErrorMessage = errorMessage };
	}
}