using BlazorComponents.Models;

namespace UiShared.Models.Blazor.Redux.Actions.UserRead;

public record UpdateAction(ReadOnlyUser? UpdatedUserRead, TimeSpan Elapsed);