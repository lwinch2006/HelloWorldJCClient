using Fluxor;
using UiShared.Models.Blazor.Redux.Actions.UserRead;
using UiShared.Models.Blazor.Redux.Stores;

namespace UiShared.Models.Blazor.Redux.Reducers.UserRead;

public static partial class AppReducer
{
	[ReducerMethod(typeof(ClearAction))]
	public static AppStore ReduceClearAction(AppStore appStore)
	{
		return appStore with { IsLoading = false, UserRead = null, Elapsed = TimeSpan.Zero, ErrorMessage = null };
	}
}