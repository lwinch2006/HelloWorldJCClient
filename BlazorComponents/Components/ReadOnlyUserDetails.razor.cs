using BlazorComponents.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorComponents.Components;

public partial class ReadOnlyUserDetails : ComponentBase
{
	private const string DefaultImageUrl = "_content/Dka.Net.HelloWorldJCClient.BlazorComponents/images/default-photo.webp";

	private string _imageDataUrl = default!;

	private string? _firstName;
	private string? _lastName;
	private string? _email;
	private string? _phone;
	private string? _photo;
	private string? _elapsed;

	[Parameter] public string? CssStyle { get; set; }

	[Parameter] public ReadOnlyUser? User { get; set; }

	[Parameter] public TimeSpan Time { get; set; }

	[Parameter] public Action? Refresh { get; set; }

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		if (User == null)
		{
			InitDefaultValues();
			return;
		}

		(_firstName, _lastName, _email, _phone, _) = User;
		_photo = User.Photo?.Length > 0 ? "yes" : "no";
		_elapsed = $"Time: {Time.TotalSeconds} seconds";
		_imageDataUrl = GetImageDataUrl(User);
	}

	private string GetImageDataUrl(ReadOnlyUser? user)
	{
		if (user?.Photo?.Length > 0 != true)
		{
			return DefaultImageUrl;
		}

		var imageAsBase64 = Convert.ToBase64String(user.Photo);
		return $"data:image/webp;base64,{imageAsBase64}";
	}

	private void InitDefaultValues()
	{
		(_firstName, _lastName, _email, _phone, _photo, _elapsed) = (null, null, null, null, null, null);
		_imageDataUrl = DefaultImageUrl;
	}
}