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

    public sealed class WcfInputAdapterFactory : IInputAdapterFactory<WcfAdapterConfig>, IDeclareAdvanceTimeProperties<WcfAdapterConfig>
    {
        public InputAdapterBase Create(WcfAdapterConfig configInfo, EventShape eventShape, CepEventType cepEventType)
        {
            InputAdapterBase inputAdapter;

            switch (eventShape)
            {
                case EventShape.Point:
                    {
                        inputAdapter = new WcfPointInputAdapter(cepEventType, configInfo);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }

            return inputAdapter;
        }

        public void Dispose()
        {
        }


        #region IDeclareAdvanceTimeProperties<Uri> Members

        public AdapterAdvanceTimeSettings DeclareAdvanceTimeProperties(WcfAdapterConfig configInfo, EventShape eventShape, CepEventType cepEventType)
        {
            return new AdapterAdvanceTimeSettings(
                new AdvanceTimeGenerationSettings(1, TimeSpan.FromTicks(-1)),
                AdvanceTimePolicy.Drop);
        }

        #endregion
    }
}
