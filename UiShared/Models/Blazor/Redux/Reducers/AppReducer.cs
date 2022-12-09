using Fluxor;
using UiShared.Models.Blazor.Redux.Actions;
using UiShared.Models.Blazor.Redux.Stores;

namespace UiShared.Models.Blazor.Redux.Reducers;

public static class AppReducer
{
	[ReducerMethod(typeof(ClearUserReadAction))]
	public static AppStore ReduceClearUserReadAction(AppStore appStore)
	{
		return appStore with { UserRead = null, Elapsed = TimeSpan.Zero };
	}
	
	[ReducerMethod]
	public static AppStore ReduceUpdateUserReadAction(AppStore appStore, UpdateUserReadAction action)
	{
		return appStore with { IsLoading = false, UserRead = action.UpdatedUserRead, Elapsed = action.Elapsed };
	}

	[ReducerMethod(typeof(ReadDummyUserFromCardAction))]
	public static AppStore ReduceReadDummyUserFromCardAction(AppStore appStore)
	{
		return appStore with { IsLoading = true, CardInserted = true };
	}
}