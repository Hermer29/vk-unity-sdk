using System;
using System.Collections.Generic;

namespace VkSdk.Runtime.Internal.Library.Observables
{
    internal sealed class ObserversContainer<T>
    {
        private readonly Lazy<List<IObserver<T>>> _observers;

        public ObserversContainer()
        {
            List<IObserver<T>> ListFactory() => new List<IObserver<T>>();
            _observers = new Lazy<List<IObserver<T>>>(ListFactory);
        }

        public void Add(IObserver<T> observer)
        {
            _observers.Value.Add(observer);
        }

        public IDisposable GenerateDisposingFromList(IObserver<T> observer)
        {
            return new DisposableHelper(() => _observers.Value.Remove(observer));
        }

        public void Notify(T message)
        {
            if (_observers.IsValueCreated == false)
                return;
            
            foreach (var observer in _observers.Value)
            {
                observer.OnNext(message);
            }
        }
    }
}