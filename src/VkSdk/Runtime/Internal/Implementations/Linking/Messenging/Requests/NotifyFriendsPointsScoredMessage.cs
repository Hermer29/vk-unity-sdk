namespace VkSdk.Runtime.Internal.Implementations.Linking.Messenging.Requests
{
    internal class NotifyFriendsPointsScoredMessage : RequestMessage
    {
        public readonly int Score;

        public NotifyFriendsPointsScoredMessage(int score)
        {
            Score = score;
        }
    }
}