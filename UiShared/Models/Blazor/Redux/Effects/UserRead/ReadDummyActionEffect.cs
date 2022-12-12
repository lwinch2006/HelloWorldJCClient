using System.Diagnostics;
using BlazorComponents.Models;
using Fluxor;
using UiShared.Models.Blazor.Redux.Actions.UserRead;

namespace UiShared.Models.Blazor.Redux.Effects.UserRead;

public class ReadDummyActionEffect : Effect<ReadDummyAction>
{
	private readonly HttpClient _httpClient;
	
	public ReadDummyActionEffect(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}
	
	public override async Task HandleAsync(ReadDummyAction action, IDispatcher dispatcher)
	{
		var stopWatch = new Stopwatch();
		stopWatch.Start();
		
		var photoBytes = await _httpClient.GetByteArrayAsync("images/photo-exported-256.webp");
		var user = new ReadOnlyUser("John", "Doe", "John.Doe@test.com", "+4711223344", photoBytes);

		await Task.Delay(3000);
		
		stopWatch.Stop();
		
		var updateUserReadAction = new Actions.UserRead.UpdateAction(user, stopWatch.Elapsed);
		dispatcher.Dispatch(updateUserReadAction);
	}
}