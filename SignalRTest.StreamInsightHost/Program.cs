using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.ComplexEventProcessing;
using Microsoft.ComplexEventProcessing.Linq;
using Seroter.SI.AzureAppFabricAdapter;
using StreamInsight.Samples.Adapters.Wcf;

namespace SignalRTest.StreamInsightHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(":: Starting embedded StreamInsight server ::");

            //create SI server
            using(Server server = Server.Create("Default"))
            {
                //create SI application
                Application app = server.CreateApplication("StreamInsightSignalR.Monitor");
       
                //create input adapter configuration
                WcfAdapterConfig inConfig = new WcfAdapterConfig()
                {
                    Password = "",
                    RequireAccessToken = false,
                    Username  = "",
                    ServiceAddress = "http://localhost/StreamInsight/Default/InputAdapter"
                };

                //create output adapter configuration
                WcfAdapterConfig outConfig = new WcfAdapterConfig()
                {
                    Password = "",
                    RequireAccessToken = false,
                    Username = "",
                    ServiceAddress = "http://localhost/StreamInsight.Monitor/NotificationService.svc"
                };

                //create event stream from the source adapter
                CepStream<BizEvent> input = CepStream<BizEvent>.Create("BizEventStream", typeof(WcfInputAdapterFactory), inConfig, EventShape.Point);
                //build initial LINQ query that is a simple passthrough
                var eventQuery = from i in input
                                 select i;

                //create unbounded SI query that doesn't emit to specific adapter
                var query0 = eventQuery.ToQuery(app, "BizQueryRaw", string.Empty, EventShape.Point, StreamEventOrder.FullyOrdered);
                query0.Start();

                //create another query that latches onto previous query
                //filters out all individual web hits used in later agg query
                var eventQuery1 = from i in query0.ToStream<BizEvent>()
                                  where i.Category != "Web"
                                  select i;

                //another query that groups events by type; used here for web site hits
                var eventQuery2 = from i in query0.ToStream<BizEvent>()
                                  group i by i.Category into EventGroup
                                  from win in EventGroup.TumblingWindow(TimeSpan.FromSeconds(10))
                                  select new BizEvent
                                  {
                                      Category = EventGroup.Key,
                                      EventMessage = win.Count().ToString() + " web visits in the past 10 seconds"
                                  };
                //new query that takes result of previous and just emits web groups
                var eventQuery3 = from i in eventQuery2
                                  where i.Category == "Web"
                                  select i;

                //create new SI queries bound to WCF output adapter
                var query1 = eventQuery1.ToQuery(app, "BizQuery1", string.Empty, typeof(WcfOutputAdapterFactory), outConfig, EventShape.Point, StreamEventOrder.FullyOrdered);
                var query2 = eventQuery3.ToQuery(app, "BizQuery2", string.Empty, typeof(WcfOutputAdapterFactory), outConfig, EventShape.Point, StreamEventOrder.FullyOrdered);

                //start queries
                query1.Start();
                query2.Start();
                Console.WriteLine("Query started. Press [Enter] to stop.");

                Console.ReadLine();
                //stop all queries
                query1.Stop();
                query2.Stop();
                query0.Stop();
                Console.Write("Query stopped.");
                Console.ReadLine();

            }
        }

        private class BizEvent
        {
            public string Category { get; set; }
            public string EventMessage { get; set; }
        }
    }
}
