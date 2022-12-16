using System;
using VkSdk.Runtime.Internal.Implementations.Linking.Messenging.Requests;
using VkSdk.Runtime.Internal.Implementations.Linking.Messenging.Responses;

namespace VkSdk.Runtime.Internal.Implementations.Linking.Messenging
{
    internal interface IBackendMessenger : IObservable<RequestMessage>
    {
        void Put(ResponseMessage responseMessage);
    }
}