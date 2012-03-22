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
    using Microsoft.ComplexEventProcessing;
    using Microsoft.ComplexEventProcessing.Adapters;

    using Seroter.SI.AzureAppFabricAdapter;

    public sealed class WcfOutputAdapterFactory : IOutputAdapterFactory<WcfAdapterConfig>
    {
        public OutputAdapterBase Create(WcfAdapterConfig configInfo, EventShape eventShape, CepEventType cepEventType)
        {
            OutputAdapterBase outputAdapter;

            switch (eventShape)
            {
                case EventShape.Point:
                    {
                        outputAdapter = new WcfPointOutputAdapter(cepEventType, configInfo);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }

            return outputAdapter;
        }

        public void Dispose()
        {
        }
    }
}
