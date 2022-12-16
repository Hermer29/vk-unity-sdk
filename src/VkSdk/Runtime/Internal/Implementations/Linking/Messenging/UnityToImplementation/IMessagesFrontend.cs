using System;

namespace VkSdk.Runtime.Internal.Implementations.Linking.Messenging.UnityToImplementation
{
    internal interface IMessagesFrontend
    {
        event Action<string> RewardedEnded;
        event Action<string> RewardedShowing;
        event Action<string> RewardedError;
        event Action InterstitialShowing;
        event Action InterstitialEnded;
        event Action InterstitialError;
        void ShowRewarded(string placement);
        void ShowInterstitial();
        void NotifyFriendsLevelReached(int level);
        void NotifyFriendsPointsScored(int score);
        void NotifyFriendsMissionAccomplishedMessage(int missionIdentifier);
        void Share();
    }
}