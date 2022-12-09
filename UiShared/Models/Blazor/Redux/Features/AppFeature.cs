using Fluxor;
using UiShared.Models.Blazor.Redux.Stores;

namespace UiShared.Models.Blazor.Redux.Features;

public class AppFeature : Feature<AppStore>
{
	public override string GetName() => nameof(AppStore);

	protected override AppStore GetInitialState()
	{
		return new AppStore
		{
			IsLoading = false,
			CardInserted = false,
			Elapsed = TimeSpan.Zero,
			ErrorMessage = null,
			UserRead = null,
			UserWrite = null
		};
	}
}