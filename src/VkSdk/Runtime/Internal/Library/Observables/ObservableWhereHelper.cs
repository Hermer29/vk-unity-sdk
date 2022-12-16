using System;
using System.Collections.Generic;

namespace VkSdk.Runtime.Internal.Library.Observables
{
    internal class ObservableWhereHelper<T> : IObservable<T>, IObserver<T>
    {
        private readonly IObservable<T> _source;
        private readonly Predicate<T> _predicate;
        private readonly Lazy<List<IObserver<T>>> _observers;

        public ObservableWhereHelper(IObservable<T> source, Predicate<T> predicate)
        {
            _source = source;
            _predicate = predicate;
            List<IObserver<T>> ListFactory() => new List<IObserver<T>>();
            _observers = new Lazy<List<IObserver<T>>>(ListFactory);
        }
        
        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observers.Value.Add(observer);
            return new DisposableHelper(() => _observers.Value.Remove(observer));
        }

        public void OnCompleted()
        {
            foreach (var observer in _observers.Value)
            {
                observer.OnCompleted();
            }
        }

        public void OnError(Exception error)
        {
            foreach (var observer in _observers.Value)
            {
                observer.OnError(error);
            }
        }

        public void OnNext(T value)
        {
            if (_predicate(value) == false)
                return;
            
            foreach (var observer in _observers.Value)
            {
                observer.OnNext(value);
            }
        }
    }
}