// *********************************************************
//
//  Copyright (c) Microsoft. All rights reserved.
//  This code is licensed under the Apache 2.0 License.
//  THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OR
//  CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED,
//  INCLUDING, WITHOUT LIMITATION, ANY IMPLIED WARRANTIES
//  OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR
//  PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
// *********************************************************

namespace StreamInsight.Samples.Adapters.Wcf
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading;
    using Microsoft.ComplexEventProcessing;
    using Microsoft.ComplexEventProcessing.Adapters;

    using Seroter.SI.AzureAppFabricAdapter;

    /// <summary>
    /// Implementation of adapter contract and service contract for point event inputs.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    internal sealed class WcfPointInputAdapter : PointInputAdapter, IPointInputAdapter
    {
        private static readonly int StopPollingPeriod = 1000; // msec.
        private readonly CepEventType eventType;
        private readonly Timer timer;
        private readonly object sync;
        private readonly ServiceHost host;

        public WcfPointInputAdapter(CepEventType eventType, WcfAdapterConfig configInfo)
        {
            this.eventType = eventType;
            this.sync = new object();

            // Initialize the service host. The host is opened and closed as the adapter is started
            // and stopped.
            this.host = new ServiceHost(this);
            host.AddServiceEndpoint(typeof(IPointInputAdapter), new WSHttpBinding(), configInfo.ServiceAddress);

            // Poll the adapter to determine when it is time to stop.
            this.timer = new Timer(CheckStopping);
            this.timer.Change(StopPollingPeriod, Timeout.Infinite);
        }

        public override void Start()
        {
            this.host.Open();
        }

        public override void Resume()
        {
        }

        private void CheckStopping(object state)
        {
            lock (this.sync)
            {
                if (AdapterState == AdapterState.Stopping)
                {
                    this.host.Close();
                    Stopped();
                }
                else
                {
                    // Check again after the polling period has elapsed.
                    this.timer.Change(StopPollingPeriod, Timeout.Infinite);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((IDisposable)this.host).Dispose();
            }
            base.Dispose(disposing);
        }

        ResultCode IPointInputAdapter.EnqueueEvent(WcfPointEvent wcfPointEvent)
        {
            lock (sync)
            {
                switch (AdapterState)
                {
                    case AdapterState.Created:
                    case AdapterState.Suspended:
                        return ResultCode.Suspended;
                    case AdapterState.Stopping:
                    case AdapterState.Stopped:
                        return ResultCode.Stopped;
                }

                if (wcfPointEvent.IsInsert)
                {
                    return EnqueueInsert(wcfPointEvent.StartTime, wcfPointEvent.Payload);
                }
                else
                {
                    return EnqueueCti(wcfPointEvent.StartTime);
                }
            }
        }

        private ResultCode EnqueueCti(DateTimeOffset startTime)
        {
            if (base.EnqueueCtiEvent(startTime) == EnqueueOperationResult.Full)
            {
                Ready();
                return ResultCode.Suspended;
            }
            return ResultCode.Success;
        }

        private ResultCode EnqueueInsert(DateTimeOffset startTime, Dictionary<string, object> payload)
        {
            if (null == payload)
            {
                throw new ArgumentNullException("payload");
            }

            PointEvent pointEvent = base.CreateInsertEvent();
            if (null == pointEvent)
            {
                Ready();
                return ResultCode.Suspended;
            }

            try
            {
                foreach (KeyValuePair<string, object> keyAndValue in payload)
                {
                    int ordinal = this.eventType.Fields[keyAndValue.Key].Ordinal;
                    pointEvent.SetField(ordinal, keyAndValue.Value);
                }
                pointEvent.StartTime = startTime;


                if (Enqueue(ref pointEvent) == EnqueueOperationResult.Full)
                {
                    Ready();
                    return ResultCode.Suspended;
                }
                return ResultCode.Success;
            }
            finally
            {
                if (null != pointEvent)
                {
                    ReleaseEvent(ref pointEvent);
                }
            }
        }
    }
}