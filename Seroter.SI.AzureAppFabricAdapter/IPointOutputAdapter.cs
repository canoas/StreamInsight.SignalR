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
    using System.ServiceModel;

    /// <summary>
    /// Service contract for the point output adapter.
    /// </summary>
    [ServiceContract]
    public interface IPointOutputAdapter
    {
        /// <summary>
        /// Attempts to dequeue a given point event. The result code indicates whether the operation
        /// has succeeded, the adapter is suspended -- in which case the operation should be retried later --
        /// or whether the adapter has stopped and will no longer return events.
        /// </summary>
        [OperationContract]
        ResultCode DequeueEvent(out WcfPointEvent result);
    }
}
