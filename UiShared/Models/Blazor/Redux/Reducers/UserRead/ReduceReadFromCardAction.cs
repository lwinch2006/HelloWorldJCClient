using Fluxor;
using UiShared.Models.Blazor.Redux.Actions.UserRead;
using UiShared.Models.Blazor.Redux.Stores;

namespace UiShared.Models.Blazor.Redux.Reducers.UserRead;

public static partial class AppReducer
{
	[ReducerMethod(typeof(ReadFromCardAction))]
	public static AppStore ReduceReadFromCardAction(AppStore appStore)
	{
		return appStore with { IsLoading = true, CardInserted = true };
	}
}