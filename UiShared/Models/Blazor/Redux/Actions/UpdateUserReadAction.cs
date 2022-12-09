using BlazorComponents.Models;

namespace UiShared.Models.Blazor.Redux.Actions;

public record UpdateUserReadAction(ReadOnlyUser? UpdatedUserRead, TimeSpan Elapsed);