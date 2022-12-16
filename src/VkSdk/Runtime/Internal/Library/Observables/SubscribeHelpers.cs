using System;

namespace VkSdk.Runtime.Internal.Library.Observables
{
    public class ObservableSubscribeHelper<T> : IObserver<T>, IDisposable
    {
        private readonly IObservable<T> _source;
        private readonly Action<T> _onNext;
        private readonly IDisposable _connection;

        public ObservableSubscribeHelper(IObservable<T> source, Action<T> onNext)
        {
            _source = source;
            _onNext = onNext;

            _source.Subscribe(this);
        }
        
        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnNext(T value)
        {
            _onNext.Invoke(value);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}