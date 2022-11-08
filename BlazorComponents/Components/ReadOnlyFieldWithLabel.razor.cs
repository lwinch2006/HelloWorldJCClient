using Microsoft.AspNetCore.Components;

namespace BlazorComponents.Components;

public partial class ReadOnlyFieldWithLabel : ComponentBase
{
	private readonly string _inputFieldId = $"input-{Guid.NewGuid():D}";

	[Parameter] public string? Label { get; set; }

	[Parameter] public string? Type { get; set; }

	[Parameter] public string? Value { get; set; }

	[Parameter] public string? Placeholder { get; set; }
}