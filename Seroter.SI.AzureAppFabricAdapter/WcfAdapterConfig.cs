using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seroter.SI.AzureAppFabricAdapter
{
    public struct WcfAdapterConfig
    {
        public string ServiceAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RequireAccessToken { get; set; }

        //consider TODO: binding choice

    }
}
