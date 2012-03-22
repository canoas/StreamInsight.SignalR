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
    /// <summary>
    /// Result code for WCF adapter tasks.
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// Indicates the task has succeeded.
        /// </summary>
        Success,

        /// <summary>
        /// Indicates that the adapter is suspended and that the task should
        /// be retried at a later time.
        /// </summary>
        Suspended,

        /// <summary>
        /// Indicates that the adapter has stopped or is stopping.
        /// </summary>
        Stopped,
    }
}
