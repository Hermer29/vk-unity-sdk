namespace VkSdk.Runtime.Internal.Implementations.Linking.Messenging.Requests
{
    internal class NotifyFriendsLevelReachedMessage : RequestMessage
    {
        public readonly int Level;

        public NotifyFriendsLevelReachedMessage(int level)
        {
            Level = level;
        }
    }
}