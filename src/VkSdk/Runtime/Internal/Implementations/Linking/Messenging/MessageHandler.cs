using System;

namespace VkSdk.Runtime.Internal.Implementations.Linking.Messenging
{
    public abstract class MessageHandler
    {
        public abstract Type HandlingMessageType { get; }

        public void Handle<T>(T message)
        {
            if (typeof(T) != HandlingMessageType)
                throw new InvalidOperationException();
            
            ExecuteAction(message);
        }

        protected abstract void ExecuteAction<T>(T value);
    }
}