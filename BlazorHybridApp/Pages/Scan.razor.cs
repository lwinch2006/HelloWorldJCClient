using System.Diagnostics;
using BlazorComponents.Models;
using BusinessLogic;
using Infrastructure;
using JCClientCore;
using JCClientCore.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorHybridApp.Pages;

public partial class Scan : ComponentBase
{
	private ReadOnlyUser? _userVm;

	private bool _isProgressing;
	private string? _errorMessage;
	private TimeSpan _elapsed;

	private IJCClient _jcMonitorClient = default!;
	
	protected override void OnInitialized()
	{
		try
		{
			_jcMonitorClient = new JCClient();
			_jcMonitorClient.StartCardMonitor(() => Task.WaitAll(Refresh()), () => Task.WaitAll(ClearWindow()));
		}
		catch (Exception ex)
		{
			_errorMessage = "JCClient initialization error";
			_errorMessage = ex.ToString();
		}
	}
	
	private async Task ClearWindow()
	{
		_userVm = null;
		await InvokeAsync(StateHasChanged);
	}
	
	private async Task Refresh()
	{
		_isProgressing = true;
		await InvokeAsync(StateHasChanged);
			
		_jcMonitorClient.StopCardMonitor();
		await GetUserAndMeasureTime();
		_jcMonitorClient.StartCardMonitor(() => Task.WaitAll(Refresh()), () => Task.WaitAll(ClearWindow()));
	}

	private async Task GetUserAndMeasureTime()
	{
		try
		{
			var stopWatch = new Stopwatch();
			stopWatch.Start();
			
			var userService = new UserService(new JCUserRepository(_jcMonitorClient));
			var user = userService.GetUser();

			_userVm = null;
			if (user != null)
			{
				_userVm = new ReadOnlyUser(user.FirstName, user.Lastname, user.Email, user.Phone, user.Photo);
			}
			
			stopWatch.Stop();
			_elapsed = stopWatch.Elapsed;
		}
		catch (CardOperationException ex)
		{
			_errorMessage = ex.Message;
		}
		
		_isProgressing = false;
		await InvokeAsync(StateHasChanged);
	}
}