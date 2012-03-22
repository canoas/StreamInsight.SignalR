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
    using System.Runtime.Serialization;
    using Microsoft.ComplexEventProcessing;

    /// <summary>
    /// Serializable representation of a point event.
    /// </summary>
    [DataContract]
    public struct WcfPointEvent
    {
        /// <summary>
        /// Gets the event payload in the form of key-value pairs.
        /// </summary>
        [DataMember]
        public Dictionary<string, object> Payload { get; set; }
        
        /// <summary>
        /// Gets the start time for the event.
        /// </summary>
        [DataMember]
        public DateTimeOffset StartTime { get; set; }
        
        /// <summary>
        /// Gets a value indicating whether the event is an insert or a CTI.
        /// </summary>
        [DataMember]
        public bool IsInsert { get; set; }
    }
}