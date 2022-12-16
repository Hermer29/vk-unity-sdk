using System;

namespace VkSdk.Runtime.Internal.Library.Observables
{
    public static class ObservableExtensions
    {
        public static IObservable<T> Where<T>(this IObservable<T> source, Predicate<T> predicate)
        {
            return new ObservableWhereHelper<T>(source, predicate);
        }

        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext)
        { 
            return new ObservableSubscribeHelper<T>(source, onNext);
        }
    }
}