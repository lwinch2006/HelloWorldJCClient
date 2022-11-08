using Microsoft.AspNetCore.Components;

namespace BlazorComponents.Components;

public partial class ReadOnlyUserDetails : ComponentBase
{
	public record UserVm(string? FirstName, string? LastName, string? Email, string? Phone, byte[]? Photo);

	private const string DefaultImageUrl = "_content/BlazorComponents/images/default-photo.webp";

	private string _imageDataUrl = default!;

	private string? _firstName;
	private string? _lastName;
	private string? _email;
	private string? _phone;
	private string? _photo;
	private string? _elapsed;

	[Parameter] public string? CssStyle { get; set; }

	[Parameter] public UserVm? User { get; set; }

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

	private string GetImageDataUrl(UserVm? user)
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