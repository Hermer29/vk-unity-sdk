namespace VkSdk.Runtime.Internal.Implementations.Linking.Messenging.Responses
{
    internal class ResponseMessage
    {
        public readonly ResponseType ResponseType;

        protected ResponseMessage(ResponseType responseType)
        {
            ResponseType = responseType;
        }
    }
}