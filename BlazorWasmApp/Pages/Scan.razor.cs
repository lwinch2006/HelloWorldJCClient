using System.Diagnostics;
using BlazorComponents.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmApp.Pages;

public partial class Scan : ComponentBase
{
	private bool _isProgressing;
	private string? _errorMessage;
	private ReadOnlyUserDetails.UserVm? _user;
	private TimeSpan _elapsed;

	private bool _sampleUser = false;

	[Inject] private HttpClient Http { get; init; } = default!;

	protected override async Task OnInitializedAsync()
	{
		await InitDefaultUserAndImage();
		await base.OnInitializedAsync();
	}
	
	private async Task InitDefaultUserAndImage(bool sampleUser = false)
	{
		if (!sampleUser)
		{
			_user = null;
			return;
		}

		var photoBytes = await Http.GetByteArrayAsync("images/photo-exported-256.webp");
		_user = new ReadOnlyUserDetails.UserVm("John", "Doe", "John.Doe@test.com", "+4711223344", photoBytes);
	}

	private async Task Refresh()
	{
		_sampleUser = !_sampleUser;
		
		var stopWatch = new Stopwatch();
		stopWatch.Start();
		
		_isProgressing = true;
		await InvokeAsync(StateHasChanged);

		await Task.Delay(3000);
		
		await InitDefaultUserAndImage(_sampleUser);

		stopWatch.Stop();
		_elapsed = stopWatch.Elapsed;
		
		_isProgressing = false;
		await InvokeAsync(StateHasChanged);
	}
}