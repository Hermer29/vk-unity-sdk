namespace VkSdk.Runtime.Internal.Implementations.Linking.Messenging.Responses
{
    internal class RewardedResponseMessage : ResponseMessage
    {
        public readonly string AdvertisingPlacement;
        
        public RewardedResponseMessage(string advertisingPlacement, ResponseType type) : base(type)
        {
            AdvertisingPlacement = advertisingPlacement;
        }
    }
}