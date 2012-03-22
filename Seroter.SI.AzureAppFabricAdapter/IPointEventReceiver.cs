namespace StreamInsight.Samples.Adapters.Wcf
{
    using System.ServiceModel;

    /// <summary>
    /// Service contract for the point output adapter.
    /// </summary>
    [ServiceContract]
    public interface IPointEventReceiver
    {
        /// <summary>
        /// Attempts to dequeue a given point event. The result code indicates whether the operation
        /// has succeeded, the adapter is suspended -- in which case the operation should be retried later --
        /// or whether the adapter has stopped and will no longer return events.
        /// </summary>
        [OperationContract]
        ResultCode PublishEvent(WcfPointEvent result);
    }
}
