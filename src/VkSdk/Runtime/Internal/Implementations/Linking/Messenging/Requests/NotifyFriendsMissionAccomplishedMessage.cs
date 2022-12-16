namespace VkSdk.Runtime.Internal.Implementations.Linking.Messenging.Requests
{
    internal class NotifyFriendsMissionAccomplishedMessage : RequestMessage
    {
        public readonly int MissionIdentifier;

        public NotifyFriendsMissionAccomplishedMessage(int missionIdentifier)
        {
            MissionIdentifier = missionIdentifier;
        }
    }
}