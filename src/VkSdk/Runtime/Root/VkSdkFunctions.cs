using System;
using UnityEngine;
using VkSdk.Runtime.Internal.Implementations.Linking.Messenging.UnityToImplementation;

namespace VkSdk.Runtime.Root
{
    public class VkSdkFunctions : MonoBehaviour, IMessagesFrontend
    {
        private static IMessagesFrontend _frontend;
        private static VkSdkFunctions _instance;
        
        internal void Construct(IMessagesFrontend frontend)
        {
            _frontend = frontend;
            _instance = this;
        }

        public static VkSdkFunctions Instance => _instance;


        public event Action<string> RewardedEnded
        {
            add => _frontend.RewardedEnded += value;
            remove => _frontend.RewardedEnded -= value;
        }

        public event Action<string> RewardedShowing
        {
            add => _frontend.RewardedShowing += value;
            remove => _frontend.RewardedShowing -= value;
        }

        public event Action<string> RewardedError
        {
            add => _frontend.RewardedError += value;
            remove => _frontend.RewardedError -= value;
        }

        public event Action InterstitialShowing
        {
            add => _frontend.InterstitialShowing += value;
            remove => _frontend.InterstitialShowing -= value;
        }

        public event Action InterstitialEnded
        {
            add => _frontend.InterstitialEnded += value;
            remove => _frontend.InterstitialEnded -= value;
        }

        public event Action InterstitialError
        {
            add => _frontend.InterstitialError += value;
            remove => _frontend.InterstitialError -= value;
        }

        public void ShowRewarded(string placement)
        {
            _frontend.ShowRewarded(placement);
        }

        public void ShowInterstitial()
        {
            _frontend.ShowInterstitial();
        }

        public void NotifyFriendsLevelReached(int level)
        {
            _frontend.NotifyFriendsLevelReached(level);
        }

        public void NotifyFriendsPointsScored(int score)
        {
            _frontend.NotifyFriendsPointsScored(score);
        }

        public void NotifyFriendsMissionAccomplishedMessage(int missionIdentifier)
        {
            _frontend.NotifyFriendsMissionAccomplishedMessage(missionIdentifier);
        }

        public void Share()
        {
            _frontend.Share();
        }
    }
}