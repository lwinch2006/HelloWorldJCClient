using Fluxor;
using UiShared.Models.Blazor.Redux.Actions.UserRead;
using UiShared.Models.Blazor.Redux.Stores;

namespace UiShared.Models.Blazor.Redux.Reducers.UserRead;

public static partial class AppReducer
{
	[ReducerMethod]
	public static AppStore ReduceUpdateAction(AppStore appStore, UpdateAction action)
	{
		return appStore with { IsLoading = false, UserRead = action.UpdatedUserRead, Elapsed = action.Elapsed, ErrorMessage = null };
	}
}