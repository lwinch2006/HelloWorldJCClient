using BlazorComponents.Models;
using Fluxor;
using UiShared.Models.Blazor.Redux.Actions.UserWrite;
using UiShared.Models.Blazor.Redux.Stores;

namespace UiShared.Models.Blazor.Redux.Reducers.UserWrite;

public static partial class AppReducer
{
	[ReducerMethod(typeof(InitAction))]
	public static AppStore ReduceInitAction(AppStore appStore)
	{
		User? userWrite = null;
		
		if (appStore.UserRead is not null)
		{
			userWrite = new User
			{
				FirstName = appStore.UserRead.FirstName,
				LastName = appStore.UserRead.LastName,
				Email = appStore.UserRead.Email,
				Phone = appStore.UserRead.Phone,
				Photo = appStore.UserRead.Photo
			};
		}

		return appStore with { UserWrite = userWrite };
	}
}