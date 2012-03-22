using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using StreamInsight.Samples.Adapters.Wcf;

[ServiceContract]
public interface INotificationService
{
	[OperationContract]
	void SendEvent(string category, string eventMessage);
}
