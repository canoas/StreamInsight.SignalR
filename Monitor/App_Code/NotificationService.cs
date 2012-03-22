using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using SignalR;
using SignalR.Infrastructure;
using SignalR.Hosting.AspNet;
using StreamInsight.Samples.Adapters.Wcf;
using Seroter.SI.AzureAppFabricAdapter;

public class NotificationService : IPointEventReceiver
{
	
	public void SendEvent(string category, string eventMessage)
	{
		IConnectionManager mgr = AspNetHost.DependencyResolver.Resolve<IConnectionManager>();
		dynamic clients = mgr.GetClients<BizEventController>();

		//todo add filters for groups
		clients[category].addEventMsg(eventMessage);
	}

	//implement the operation included in interface definition
	public ResultCode PublishEvent(WcfPointEvent result)
	{
		//get category from key/value payload
		string cat = result.Payload["Category"].ToString();
		//get message from key/value payload
		string msg = result.Payload["EventMessage"].ToString();

		//get SignalR connection manager
		IConnectionManager mgr = AspNetHost.DependencyResolver.Resolve<IConnectionManager>();
		//retrieve list of all connected clients
		dynamic clients = mgr.GetClients<BizEventController>();

		//send message to all clients for given category
		clients[cat].addEventMsg(msg);
		//also send message to anyone subscribed to all events
		clients["All"].addEventMsg(msg);

		return ResultCode.Success;
	}
}
