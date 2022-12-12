using JCClientCore.Models;
using PCSC.Iso7816;

namespace JCClientCore;

public interface IJCClient
{
	bool IsResponseSuccess(Response cardResponse);
	bool IsGetResponseAnswer(Response cardResponse);
	void StartCardMonitor();
	void StartCardMonitor(Action cardInsertedEventHandler, Action cardRemovedEventHandler);
	void StopCardMonitor();
	void SelectApplet(string aid, string delimiter = ":");
	bool TrySelectApplet(string aid, string delimiter = ":");
	byte[] GetCardData(ApduParameters apduParameters);
	byte[] TryGetCardData(ApduParameters apduParameters);
}