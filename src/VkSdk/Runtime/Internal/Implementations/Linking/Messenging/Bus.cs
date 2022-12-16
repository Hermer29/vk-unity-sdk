using System;
using VkSdk.Runtime.Internal.Implementations.Linking.Messenging.Requests;
using VkSdk.Runtime.Internal.Implementations.Linking.Messenging.Responses;
using VkSdk.Runtime.Internal.Library.Observables;

namespace VkSdk.Runtime.Internal.Implementations.Linking.Messenging
{
    internal class Bus : IBackendMessenger, IFrontendMessenger
    {
        private readonly ObserversContainer<ResponseMessage> _frontendObservers;
        private readonly ObserversContainer<RequestMessage> _backendObservers;

        public Bus()
        {
            _frontendObservers = new ObserversContainer<ResponseMessage>();
            _backendObservers = new ObserversContainer<RequestMessage>();
        }

        public IDisposable Subscribe(IObserver<ResponseMessage> observer)
        {
            _frontendObservers.Add(observer);
            return _frontendObservers.GenerateDisposingFromList(observer);
        }

        void IFrontendMessenger.Put(RequestMessage message)
        {
            _backendObservers.Notify(message);
        }

        void IBackendMessenger.Put(ResponseMessage responseMessage)
        {
            _frontendObservers.Notify(responseMessage);
        }

        public IDisposable Subscribe(IObserver<RequestMessage> observer)
        {
            _backendObservers.Add(observer);
            return _backendObservers.GenerateDisposingFromList(observer);
        }
    }
}