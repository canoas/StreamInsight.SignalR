using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//added reference to SignalR
using SignalR.Hubs;

/// <summary>
/// Summary description for BizEventController
/// </summary>
public class BizEventController : Hub
{
    public void AddSubscription(string eventType)
    {
        AddToGroup(eventType);
    }
}