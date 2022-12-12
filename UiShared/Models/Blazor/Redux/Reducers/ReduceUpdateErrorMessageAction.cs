using Fluxor;
using UiShared.Models.Blazor.Redux.Actions;
using UiShared.Models.Blazor.Redux.Stores;

namespace UiShared.Models.Blazor.Redux.Reducers;

public static partial class AppReducer
{
	[ReducerMethod]
	public static AppStore ReduceUpdateErrorMessageAction(AppStore appStore, UpdateErrorMessageAction action)
	{
		return appStore with { IsLoading = false, UserRead = null, Elapsed = TimeSpan.Zero, ErrorMessage = action.ErrorMessage };
	}
}