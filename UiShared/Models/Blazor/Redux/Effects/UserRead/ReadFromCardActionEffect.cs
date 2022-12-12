using System.Diagnostics;
using BlazorComponents.Models;
using BusinessLogic;
using Fluxor;
using JCClientCore;
using JCClientCore.Models;
using UiShared.Models.Blazor.Redux.Actions;
using UiShared.Models.Blazor.Redux.Actions.UserRead;

namespace UiShared.Models.Blazor.Redux.Effects.UserRead;

public class ReadFromCardActionEffect : Effect<ReadFromCardAction>
{
	private readonly IJCClient _jcClient;
	private readonly IUserService _userService;

	public ReadFromCardActionEffect(
		IJCClient jcClient,
		IUserService userService)
	{
		_jcClient = jcClient;
		_userService = userService;
	}
	
	public override async Task HandleAsync(ReadFromCardAction action, IDispatcher dispatcher)
	{
		try
		{
			_jcClient.StopCardMonitor();

			await Task.Delay(10);

			var stopWatch = new Stopwatch();
			stopWatch.Start();

			var user = _userService.GetUser();
			var userVm = user == null
				? null
				: new ReadOnlyUser(user.FirstName, user.Lastname, user.Email, user.Phone, user.Photo);

			stopWatch.Stop();

			var updateUserReadAction = new Actions.UserRead.UpdateAction(userVm, stopWatch.Elapsed);
			dispatcher.Dispatch(updateUserReadAction);
		}
		catch (CardOperationException ex)
		{
			dispatcher.Dispatch(new UpdateErrorMessageAction(ex.Message));
		}
		finally
		{
			_jcClient.StartCardMonitor();			
		}
	}
}