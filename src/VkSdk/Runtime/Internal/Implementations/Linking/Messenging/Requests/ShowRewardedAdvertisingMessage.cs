namespace VkSdk.Runtime.Internal.Implementations.Linking.Messenging.Requests
{
    internal class ShowRewardedAdvertisingMessage : RequestMessage
    {
        public readonly string Placement;

        public ShowRewardedAdvertisingMessage(string placement)
        {
            Placement = placement;
        }
    }
}