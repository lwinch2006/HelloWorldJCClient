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

		byte[]? photoBytes = null; 
		
		try
		{
			await using var stream = GetType().Assembly.GetManifestResourceStream("DummyPersonPhoto")!;
			using var ms = new MemoryStream();
			await stream.CopyToAsync(ms);
			ms.Close();
			photoBytes = ms.ToArray();
		}
		catch {}
		
		var user = new ReadOnlyUser("John", "Doe", "John.Doe@test.com", "+4711223344", photoBytes);

		await Task.Delay(3000);
		
		stopWatch.Stop();
		
		var updateUserReadAction = new Actions.UserRead.UpdateAction(user, stopWatch.Elapsed);
		dispatcher.Dispatch(updateUserReadAction);
	}
}