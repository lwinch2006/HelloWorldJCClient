using BlazorComponents.Models;

namespace UiShared.Models.Blazor.Redux.Stores;

public record AppStore
{
	public bool IsLoading { get; init; }
	public bool CardInserted { get; init; }
	public TimeSpan Elapsed { get; init; }
	public string? ErrorMessage { get; init; }
	public ReadOnlyUser? UserRead { get; init; }
	public User? UserWrite { get; init; }
}